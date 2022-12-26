namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class RoundBracketsClosure : IClosure
    {
        public char Begin { get; } = '(';
        public char End { get; } = ')';
    }
}