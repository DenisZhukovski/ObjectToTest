using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class Groups<T> : IEnumerable<IEnumerable<T>>
    {
        private readonly IEnumerable<T> _source;
        private readonly int _groupSize;

        public Groups(IEnumerable<T> source, int groupSize)
        {
            _source = source;
            _groupSize = groupSize;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            var result = new List<T>();
            foreach (var current in _source)
            {
                result.Add(current);

                if (result.Count == _groupSize)
                {
                    yield return result.ToArray();

                    result.Clear();
                }
            }

            if (result.Any())
            {
                yield return result.ToArray();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}