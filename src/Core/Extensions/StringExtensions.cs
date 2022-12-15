using System;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using static ObjectToTest.CodeFormatting.Formatting.Join;

namespace ObjectToTest.Extensions
{
    public static class StringExtensions
    {
        public static bool SameVariable(this string fromTypeName, string fromParamName)
        {
            fromTypeName = fromTypeName.Replace("_", string.Empty);
            fromParamName = fromParamName.Replace("_", string.Empty);
            return fromTypeName.Equals(fromParamName, StringComparison.InvariantCultureIgnoreCase);
        }

        public static JointString ToJointString<T>(this IEnumerable<T> self)
        {
            return new JointString(self.Select(x => x.ToString()).ToArray());
        }

        public static string If(this string value, bool condition)
        {
            if (condition)
            {
                return value;
            }

            return string.Empty;
        }

        public static Join FormatEach(this string[] items, Func<ItemInfo, string> formatEach) => new(items, formatEach);
    }
}

