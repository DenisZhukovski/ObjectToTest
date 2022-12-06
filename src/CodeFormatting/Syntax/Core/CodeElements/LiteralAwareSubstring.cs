using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class LiteralAwareSubstring : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly string _pattern;

        public LiteralAwareSubstring(string source, string pattern)
        {
            _source = source;
            _pattern = pattern;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var allMatches = new Substrings(_source, _pattern);
            var literals = new LiteralSubstrings(_source);

            var matches = allMatches.ExcludeOnesThatAreIn(literals);

            return matches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}