using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class StringsWithSemicolonIfMultiline : IEnumerable<string>
    {
        private readonly string _source;

        public StringsWithSemicolonIfMultiline(string source)
        {
            _source = source;
        }

        public IEnumerator<string> GetEnumerator()
        {
            var items = new SemicolonSeparatedString(_source).ToArray();
            if (items.Length == 1)
            {
                yield return items.ElementAt(0);
            }
            else
            {
                foreach (var item in items)
                {
                    yield return new StringWithSemicolonInTheEnd(item).ToString();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}