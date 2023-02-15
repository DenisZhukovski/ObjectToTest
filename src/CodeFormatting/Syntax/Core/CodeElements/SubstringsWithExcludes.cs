using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class SubstringsWithExcludes : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly string _pattern;
        private readonly IEnumerable<ISubstring> _notAnalyzeIn;

        public SubstringsWithExcludes(string source, string pattern, IEnumerable<ISubstring> notAnalyzeIn)
        {
            _source = source;
            _pattern = pattern;
            _notAnalyzeIn = notAnalyzeIn;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var allMatches = new Substrings(_source, _pattern);

            var matches = allMatches.ExcludeOnesThatAreIn(_notAnalyzeIn);

            return matches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}