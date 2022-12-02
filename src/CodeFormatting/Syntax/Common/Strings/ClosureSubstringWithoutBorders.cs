namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class ClosureSubstringWithoutBorders
    {
        private readonly ISubstring _substring;

        public ClosureSubstringWithoutBorders(ISubstring substring)
        {
            _substring = substring;
        }
        
        public override string ToString()
        {
            return new SubstringWithoutOutOfBounds(_substring.ToString(), 1, _substring.ToString().Length - 2).ToString();
        }
    }
}