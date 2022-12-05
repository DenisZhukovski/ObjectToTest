using System;
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
    }
}

