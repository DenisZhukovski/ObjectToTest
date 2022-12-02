using System.Collections;
using System.Collections.Generic;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class IndexesOfCharExcludingBackslashed : IEnumerable<int>
    {
        private readonly string _text;
        private readonly char _desired;

        public IndexesOfCharExcludingBackslashed(string text, char desired)
        {
            _text = text;
            _desired = desired;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var index = 0;
            while (index < _text.Length)
            {
                if (_text[index] == _desired)
                {
                    yield return index;
                }

                if (_text[index] == '\\')
                {
                    index += 2; // skip current and next
                }
                else
                {
                    index++;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}