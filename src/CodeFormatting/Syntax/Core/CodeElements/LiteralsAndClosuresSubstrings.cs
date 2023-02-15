using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class LiteralsAndClosuresSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly IClosure[] _closures;

        public LiteralsAndClosuresSubstrings(string source, params IClosure[] closures)
        {
            _source = source;
            _closures = closures.Any()
                ? closures
                : new IClosure[]
                {
                    new RoundBracketsClosure(),
                    new SquareBracketsClosure(),
                    new CurlyBracketsClosure(),
                    new AngleBracketsClosure()
                };
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            foreach (var closure in _closures)
            {
                foreach (var closureSubstring in new LiteralAwareClosureSubstrings(_source, closure))
                {
                    yield return closureSubstring;
                }
            }

            foreach (var literal in new LiteralSubstrings(_source))
            {
                yield return literal;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}