using System.Collections;
using System.Collections.Generic;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class LiteralAwareClosureSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly char _begin;
        private readonly char _end;

        public LiteralAwareClosureSubstrings(string source, char begin, char end)
        {
            _source = source;
            _begin = begin;
            _end = end;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            return new ClosureSubstrings(_source, _begin, _end, notAnalyzeIn: new LiteralSubstrings(_source)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}