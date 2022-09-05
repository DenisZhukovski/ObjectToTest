using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.Exceptions;
using ObjectToTest.Extensions;

namespace ObjectToTest
{
    public static class ObjectExtensions
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

        public static string ToTest(this object @object)
        {
            return new ObjectAsConstructor(
                @object ?? throw new ArgumentNullException(nameof(@object))
            ).ToString();
        }

        internal static string Join(this IList<object> arguments)
        {
            return string.Join(",\r\n", arguments.Select(arg => arg.ToTest()));
        }

        internal static string ToStringForInialization(this object @object)
        {
            string valueStr;
            if (@object is null)
            {
                valueStr = "null";
            }
            else if (@object.IsPrimitive())
            {
                if (@object is string)
                {
                    return $"\"{@object}\"";
                }
                valueStr = @object.ToString();
            }
            else
            {
                valueStr = @object.ToTest();
            }

            return valueStr;
        }

        internal static string GenericTypeName(this Type type)
        {
            var genericArguments = type.GetGenericArguments();
            var arguments = string.Join(",", genericArguments.Select(type =>
            {
                if (Aliases.ContainsKey(type))
                {
                    return Aliases[type];
                }
                return type.Name;
            }));
            return $"{type.Name.Replace($"`{genericArguments.Count()}", string.Empty)}<{arguments}>";
        }

        internal static IConstructor ValidConstructor(this object @object, IArguments sharedArguments)
        {
            if (@object.IsPrimitive())
            {
                return new ValueTypeConstructor(@object);
            }
            else if (@object.IsCollection())
            {
                return new CollectionConstructor(@object);
            }

            foreach (var constructor in @object.Constructors())
            {
                var ctor = constructor.GetParameters().Any()
                    ? new Constructors.ParameterizedConstructor(@object, constructor, sharedArguments)
                    : (IConstructor)new Constructors.DefaultConstructor(@object, sharedArguments);
                if (ctor.IsValid)
                {
                    return ctor;
                }
            }

            throw new NoConstructorException(@object.GetType());
        }
    }
}
