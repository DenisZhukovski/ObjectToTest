using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class ExcludeSubstrings : IEnumerable<ISubstring>
    {
        private readonly IEnumerable<ISubstring> _from;
        private readonly IEnumerable<ISubstring> _excludeRanges;

        public ExcludeSubstrings(IEnumerable<ISubstring> @from, IEnumerable<ISubstring> excludeRanges)
        {
            _from = @from;
            _excludeRanges = excludeRanges;
        }

        public IEnumerator<ISubstring> GetEnumerator()
        {
            foreach (var substring in _from)
            {
                if (!_excludeRanges.Any(x => new SubstringsIntersection(source: substring, target: x).SourceFullyInTarget))
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