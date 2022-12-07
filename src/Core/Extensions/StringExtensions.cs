using System;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

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
    }
}

