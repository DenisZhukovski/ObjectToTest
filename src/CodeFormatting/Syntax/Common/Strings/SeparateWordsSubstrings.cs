using System.Collections;
using System.Collections.Generic;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class SeparateWordsSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;
        private readonly IEnumerable<ISubstring> _substrings;

        public SeparateWordsSubstrings(string source, IEnumerable<ISubstring> substrings)
        {
            _source = source;
            _substrings = substrings;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            foreach (var substring in _substrings)
            {
                var before = new CodeCharacter(_source, substring.Start - 1);
                var after = new CodeCharacter(_source, substring.End + 1);

                if (before.IsNotALexemChar && after.IsNotALexemChar)
                {
                    yield return substring;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}