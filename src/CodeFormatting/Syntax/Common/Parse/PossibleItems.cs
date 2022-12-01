using System;

namespace ObjectToTest.CodeFormatting.Syntax.Common
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
                var result = match(value);

                if (result is ParseSuccessful<TOut> successful)
                {
                    return successful.Value;
                }
            }

            throw new InvalidOperationException("Last item should always return object with 'true' result.");
        }
    }
}