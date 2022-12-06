using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class LiteralSubstrings : IEnumerable<ISubstring>
    {
        private readonly string _source;

        public LiteralSubstrings(string source)
        {
            _source = source;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            var indexes = new IndexesOfCharExcludingBackslashed(_source, '"').ToArray();

            foreach (var literalGroup in new Groups<int>(indexes, 2))
            {
                if (literalGroup.Count() == 2)
                {
                    yield return new Substring(_source, literalGroup.ElementAt(0), literalGroup.ElementAt(1));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}