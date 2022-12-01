using System;

namespace ObjectToTest.CodeFormatting.Syntax.Common
{
    public class ParseBasedMatchResult<TOut>
    {
        private readonly string _value;
        private readonly Func<string, (bool, TOut)> _parse;

        public ParseBasedMatchResult(string value, Func<string, (bool, TOut)> parse)
        {
            _value = value;
            _parse = parse;
        }

        public ParseResult Unwrap()
        {
            var (result, item) = _parse(_value);

            if (result)
            {
                return new ParseSuccessful<TOut>(item);
            }

            return new ParseFail();
        }
    }
}