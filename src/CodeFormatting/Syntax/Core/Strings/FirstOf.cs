namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class FirstOf : ISubstring
    {
        private readonly ISubstring[] _substrings;

        public FirstOf(params ISubstring[] substrings)
        {
            _substrings = substrings;
        }

        public int Start => FirstOrDefault()?.Start ?? -1;

        public int End => FirstOrDefault()?.End ?? -1;

        public override string ToString()
        {
            return FirstOrDefault()?.ToString() ?? string.Empty;
        }

        private ISubstring? FirstOrDefault()
        {
            foreach (var substring in _substrings)
            {
                if (!string.IsNullOrEmpty(substring.ToString()))
                {
                    return substring;
                }
            }

            return null;
        }
    }
}