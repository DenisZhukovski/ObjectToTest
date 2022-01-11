namespace ObjectToTest.UnitTests.Extensions
{
    public static class StringExtensions
    {
        public static string NoNewLines(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return data;
            }
            return data
                .Replace("\\r\\n", string.Empty)
                .Replace("\r\n", string.Empty)
                .Replace("\\n", string.Empty)
                .Replace("\\t", string.Empty)
                .Replace("\t", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("  ", string.Empty); // in some editors tabs can be replaced with spaces
        }
    }
}
