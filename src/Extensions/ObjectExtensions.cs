using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest
{
    public static class ObjectExtensions
    {
        public static string ToTest(this object @object)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            return new ObjectAsConstructor(@object).ToString();
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
