using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
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
            return new ClosureSubstrings(
                _source,
                _begin,
                _end,
                notAnalyzeIn: new LiteralSubstrings(_source)
            ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}