using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class Substrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly string _pattern;

        public Substrings(string source, string pattern)
        {
            _source = source;
            _pattern = pattern;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var lastIndex = 0;
            int findingIndex;

            do
            {
                findingIndex = _source.IndexOf(_pattern, lastIndex, StringComparison.InvariantCultureIgnoreCase);

                if (findingIndex != -1)
                {
                    lastIndex = findingIndex + _pattern.Length;

                    yield return new Substring(_source, findingIndex, findingIndex + _pattern.Length - 1);
                }

            } while (findingIndex != -1);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}