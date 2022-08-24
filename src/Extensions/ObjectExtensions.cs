using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ObjectToTest
{
    public static class ObjectExtensions
    {
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
            else
            {
                valueStr = @object.ToString();
            }

            return valueStr;
        }
    }
}
