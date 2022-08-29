using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using ObjectToTest.ConstructorParameters;

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
            _ = @object ?? throw new ArgumentNullException(nameof(@object));
            return new ObjectAsConstructor(@object).ToString();
        }

        public static IEnumerable<ConstructorInfo> Constructors(this object @object)
        {
            _ = @object ?? throw new ArgumentNullException(nameof(@object));
            return @object.GetType()
                .GetConstructors()
                .Where(x => x.IsPublic)
                .OrderByDescending(x => x.GetParameters().Length);
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
            else if (@object is string)
            {
                valueStr = $"\"{@object}\"";
            }
            else if(@object.GetType().IsPrimitive || @object is decimal)
            {
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
    }
}
