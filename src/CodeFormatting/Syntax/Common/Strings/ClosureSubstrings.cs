using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class ClosureSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly char _begin;
        private readonly char _end;
        private readonly IEnumerable<ISubstring> _notAnalyzeIn;

        public ClosureSubstrings(string source, char begin, char end, IEnumerable<ISubstring> notAnalyzeIn)
        {
            _source = source;
            _begin = begin;
            _end = end;
            _notAnalyzeIn = notAnalyzeIn;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var index = 0;
            var deep = -1;
            var start = -1;
            while (index < _source.Length)
            {
                if (_notAnalyzeIn.Any(x => new CharIndexIntersection(index, x).InTarget))
                {
                    index++;
                    continue;
                }

                if (_source[index] == _begin)
                {
                    deep++;

                    if (deep == 0)
                    {
                        start = index;
                    }
                }

                if (_source[index] == _end)
                {
                    if (deep == 0)
                    {
                        yield return new Substring(_source, start, index);

                        start = -1;
                    }

                    deep--;
                }

                index++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}