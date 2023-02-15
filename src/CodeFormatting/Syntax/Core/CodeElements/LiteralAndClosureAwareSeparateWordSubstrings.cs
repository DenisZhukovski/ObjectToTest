using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class LiteralAndClosureAwareSeparateWordSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly string _pattern;

        public LiteralAndClosureAwareSeparateWordSubstrings(string source, string pattern)
        {
            _source = source;
            _pattern = pattern;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var matches = new SubstringsWithExcludes(_source, _pattern, notAnalyzeIn: new LiteralsAndClosuresSubstrings(_source));

            var result = matches.ExcludeNotSeparateWords(_source).ToList();

            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}