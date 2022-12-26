namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class CurlyBracketsClosure : IClosure
    {
        public char Begin { get; } = '{';
        public char End { get; } = '}';
    }
}