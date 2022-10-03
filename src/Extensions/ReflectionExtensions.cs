using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Extensions;

namespace ObjectToTest
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<ConstructorInfo> Constructors(this object @object)
        {
            _ = @object ?? throw new ArgumentNullException(nameof(@object));
            return @object.GetType()
                .GetConstructors()
                .Where(x => x.IsPublic)
                .OrderByDescending(x => x.GetParameters().Length);
        }

        public static bool Contains(this object @object, ParameterInfo parameter)
        {
            return @object.Contains(parameter.Name);
        }

        public static bool Contains(this object @object, string name)
        {
            return @object.Field(name) != null || @object.Property(name) != null;
        }
        
        public static bool ContainsDeep(this object @object, object objectToCheck)
        {
            foreach (var value in @object.Values())
            {
                if (value == objectToCheck || (value != null && !value.IsCollection() && !value.IsPrimitive() && value.ContainsDeep(objectToCheck)))
                {
                    return true;
                }
            }

            return false;
        }

        public static FieldInfo? Field(this object @object, FieldInfo field)
        {
            return @object.Field(field.Name);
        }

        public static FieldInfo? Field(this object @object, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return @object
                .GetType()
                .GetRuntimeFields()
                .FirstOrDefault(f => f.Name.SameVariable(name));
        }

        public static PropertyInfo? Property(this object @object, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return @object
                .GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name.SameVariable(name));
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
                else
                {
                    var property = @object.Property(name);
                    if (property != null)
                    {
                        return property.GetValue(@object);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Can not get value '{name}' from object '{@object}' ('{@object.GetType().Name}')",
                    ex
                );
            }

            throw new ArgumentException($"Can not get value for parameter with name {name} in type {@object.GetType().Name}");
        }

        internal static bool IsPrimitive(this object @object)
        {
            return @object is string || @object is decimal || @object.GetType().IsPrimitive;
        }

        internal static bool IsValueType(this object @object)
        {
            return @object != null && @object.GetType().IsValueType;
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

        internal static List<object?> Values(this object? @object)
        {
            if (@object == null)
            {
                return new List<object?>();
            }

            return @object
                .FieldsAndProperties()
                .Select(field => @object.Value(field))
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

