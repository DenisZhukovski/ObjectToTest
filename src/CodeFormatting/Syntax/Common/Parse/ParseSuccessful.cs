namespace ObjectToTest.CodeFormatting.Syntax.Common.Parse
{
    public class ParseSuccessful<T> : ParseSuccessful
    {
        public ParseSuccessful(T value) : base(value)
        {
            Value = value;
        }

        public T Value { get; }
    }

    public class ParseSuccessful : ParseResult
    {
        public ParseSuccessful(object value)
        {
            GenericValue = value;
        }

        public object GenericValue { get; }
    }
}