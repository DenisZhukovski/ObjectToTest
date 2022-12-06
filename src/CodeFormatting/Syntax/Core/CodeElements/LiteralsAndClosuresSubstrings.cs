using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class LiteralsAndClosuresSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;

        public LiteralsAndClosuresSubstrings(string source)
        {
            _source = source;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            foreach (var closure in new LiteralAwareClosureSubstrings(_source, '[', ']'))
            {
                yield return closure;
            }

            foreach (var closure in new LiteralAwareClosureSubstrings(_source, '(', ')'))
            {
                yield return closure;
            }

            foreach (var closure in new LiteralAwareClosureSubstrings(_source, '{', '}'))
            {
                yield return closure;
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