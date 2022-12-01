using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
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
                if (string.IsNullOrWhiteSpace(s) == false)
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
