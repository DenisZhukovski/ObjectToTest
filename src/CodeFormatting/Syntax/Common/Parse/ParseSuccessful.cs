namespace ObjectToTest.CodeFormatting.Syntax.Common.Parse
{
    public class ParseSuccessful<T> : ParseResult
    {
        public ParseSuccessful(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}