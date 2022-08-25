using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.UnitTests.Models
{
    public class WithIEnumerableInt
    {
        private readonly IEnumerable<int> _items;

        public WithIEnumerableInt(IEnumerable<int> items)
        {
            _items = items;
        }

        public IList<int> ToList()
        {
            return _items.ToList();
        }
    }
}
