using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Extensions;
using ObjectToTest.Core.Extensions;

namespace ObjectToTest
{
    public static class ReflectionExtensions
    {
        private static readonly Dictionary<Type, string> Aliases =
            new Dictionary<Type, string>()
            {
                { typeof(byte), "byte" },
                { typeof(sbyte), "sbyte" },
                { typeof(short), "short" },
                { typeof(ushort), "ushort" },
                { typeof(int), "int" },
                { typeof(uint), "uint" },
                { typeof(long), "long" },
                { typeof(ulong), "ulong" },
                { typeof(float), "float" },
                { typeof(double), "double" },
                { typeof(decimal), "decimal" },
                { typeof(object), "object" },
                { typeof(bool), "bool" },
                { typeof(char), "char" },
                { typeof(string), "string" },
                { typeof(void), "void" }
            };
        
        public static IEnumerable<ConstructorInfo> Constructors(this object @object)
        {
            _ = @object ?? throw new ArgumentNullException(nameof(@object));
            return @object.GetType()
                .GetConstructors()
                .Where(x => x.IsPublic)
                .OrderByDescending(x => x.GetParameters().Length);
        }
        
        internal static string GenericTypeName(this Type type)
        {
            var genericArguments = type.GetGenericArguments();
            var arguments = string.Join(",", genericArguments.Select(argumentType => argumentType.TypeName()));
            return $"{type.Name.Replace($"`{genericArguments.Length}", string.Empty)}<{arguments}>";
        }
        
        internal static string TypeName(this Type type)
        {
            if (Aliases.ContainsKey(type))
            {
                return Aliases[type];
            }
            return type.Name;
        }

        public static object? Default(this ParameterInfo parameter)
        {
            return parameter.ParameterType.IsValueType
                ? Activator.CreateInstance(parameter.ParameterType)
                : null;
        }
        
        public static bool Contains(this object @object, ParameterInfo parameter)
        {
            return @object.Contains(parameter.Name);
        }

        public static bool Contains(this object @object, string name)
        {
            return @object.Field(name) != null || @object.Property(name) != null;
        }

        public static bool ContainsValue(this object @object, object objectToCheck)
        {
            foreach (var value in @object.Values())
            {
                if (value == objectToCheck)
                {
                    return true;
                }
            }

            return false;
        }
        
        public static bool ContainsDeep(this object @object, object objectToCheck)
        {
            if (@object.IsPrimitive()
                || @object.IsDelegate()
                || @object.IsCollection()
                || @object.IsValueType()
                || @object.IsMetaType())
            {
                return false;
            }

            return @object.ContainsDeep(objectToCheck, new List<object>());
        }
        
        private static bool ContainsDeep(this object @object, object objectToCheck, List<object> callStackObjects)
        {
            if (@object.IsPrimitive()
                || @object.IsDelegate()
                || @object.IsCollection()
                || @object.IsValueType()
                || @object.IsMetaType())
            {
                return false;
            }
            
            // Recursive check.
            if (callStackObjects.Contains(@object))
            {
                return false;
            }
            
            callStackObjects.Add(@object);
            foreach (var value in @object.Values())
            {
                if (value == objectToCheck || (value != null && !value.IsCollection() && !value.IsPrimitive() && value.ContainsDeep(objectToCheck, callStackObjects)))
                {
                    return true;
                }
            }

            return false;
        }

        public static FieldInfo? Field(this object @object, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var type = @object.GetType();
            while (type != null)
            {
                var field = type
                    .GetRuntimeFields()
                    .FirstOrDefault(f => f.Name.SameVariable(name));
                if (field != null)
                {
                    return field;
                }
                type = type.BaseType;
            }

            return null;
        }

        public static PropertyInfo? Property(this object @object, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var type = @object.GetType();
            while (type != null)
            {
                var property = type
                    .GetProperties()
                    .FirstOrDefault(p => p.Name.SameVariable(name));
                if (property != null)
                {
                    return property;
                }
                type = type.BaseType;
            }

            return null;
        }

        public static object? Value(this object @object, MemberInfo parameter)
        {
            return @object.Value(parameter.Name);
        }

        public static object? Value(this object @object, ParameterInfo parameter)
        {
            return @object.Value(parameter.Name);
        }

        public static object? Value(this object @object, string name)
        {
            try
            {
                var field = @object.Field(name);
                if (field != null)
                {
                    return field.GetValue(@object);
                }

                var property = @object.Property(name);
                if (property != null)
                {
                    return property.GetValue(@object);
                }
            }
            catch (Exception ex)
            {
                if (ex.Contains<NotImplementedException>())
                {
                    return null;
                }

                throw new InvalidOperationException(
                    $"Can not get value '{name}' from object '{@object}' ('{@object.GetType().Name}')",
                    ex
                );
            }

            throw new ArgumentException($"Can not get value for parameter with name {name} in type {@object.GetType().Name}");
        }

        internal static bool IsPrimitive(this object @object)
        {
            return @object is string
                || @object is decimal
                || @object is Enum
                || @object.GetType().IsPrimitive;
        }

        internal static bool IsValueType(this object? @object)
        {
            return @object != null && @object.GetType().IsValueType;
        }
        
        internal static bool IsMetaType(this object? @object)
        {
            return @object != null && @object.GetType() == typeof(Type).GetType();
        }

        internal static bool IsCollection(this object @object)
        {
            return @object
                .GetType()
                .GetInterfaces()
                .Contains(typeof(IEnumerable));
        }

        internal static List<MemberInfo> FieldsAndProperties(this object? @object)
        {
            var result = new List<MemberInfo>();
            if (@object != null)
            {
                var objectType = @object.GetType();
                result.AddRange(
                    objectType
                        .GetProperties()
                        .Where(info => !info.GetIndexParameters().Any())
                );
                result.AddRange(objectType.GetRuntimeFields());
            }

            return result;
        }

        internal static List<object?> Values(this object? @object, bool fieldsOnly = false)
        {
            if (@object == null)
            {
                return new List<object?>();
            }

            if (fieldsOnly)
            {
                return @object
                    .GetType()
                    .GetRuntimeFields()
                    .Select(@object.Value)
                    .ToList();
            }

            return @object
                .FieldsAndProperties()
                .Select(@object.Value)
                .ToList();
        }
        
        internal static List<object?> Values(this object? @object, Func<object?, bool> predicate)
        {
            return @object
                .Values()
                .Where(predicate)
                .ToList();
        }

        internal static bool IsDefaultValue(this MemberInfo member, object @object)
        {
            object? defaultValue = member.GetType().IsValueType
                ? Activator.CreateInstance(member.GetType()) 
                : null;

            return defaultValue == @object;
        }
    }
}

