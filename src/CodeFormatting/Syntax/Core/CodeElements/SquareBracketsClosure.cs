namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class SquareBracketsClosure : IClosure
    {
        public char Begin { get; } = '[';
        public char End { get; } = ']';
    }
}