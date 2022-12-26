using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class CharacterSeparatedSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly char _separator;
        private readonly IEnumerable<ISubstring> _notAnalyzeIn;

        public CharacterSeparatedSubstrings(string source, char separator, IEnumerable<ISubstring> notAnalyzeIn)
        {
            _source = source;
            _separator = separator;
            _notAnalyzeIn = notAnalyzeIn;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var index = 0;
            var start = 0;
            while (index < _source.Length)
            {
                if (_notAnalyzeIn.Any(x => new CharIndexIntersection(index, x).InTarget))
                {
                    index++;
                    continue;
                }

                if (_source[index] == _separator)
                {
                    yield return new Substring(_source, start, index - 1);
                    start = index + 1;
                }

                index++;
            }

            if (start < _source.Length)
            {
                yield return new Substring(_source, start, _source.Length - 1);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}