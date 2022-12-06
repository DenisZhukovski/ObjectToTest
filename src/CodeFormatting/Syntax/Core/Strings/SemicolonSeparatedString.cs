using System.Collections;
using System.Collections.Generic;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class SemicolonSeparatedString : IEnumerable<string>
    {
        private readonly string _source;

        public SemicolonSeparatedString(string source)
        {
            _source = source;
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var s in _source.Split(';'))
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    yield return s;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
