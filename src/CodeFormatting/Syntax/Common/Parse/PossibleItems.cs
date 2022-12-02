using System;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Parse
{
    public class PossibleItems<TOut> : IPossibleItems<TOut>
    {
        private readonly Func<string, ParseResult>[] _matches;

        public PossibleItems(params Func<string, ParseResult>[] matches)
        {
            _matches = matches;
        }

        public TOut BestMatch(string value)
        {
            foreach (var match in _matches)
            {
                var resultingMatch = match(value);

                if (resultingMatch is ParseSuccessful successful && successful.GenericValue is TOut result)
                {
                    return result;
                }
            }

            throw new InvalidOperationException("Last item should always return object with 'true' result.");
        }
    }
}