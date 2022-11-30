using System.Collections.Generic;

namespace ObjectToTest.UnitTests.Models
{
    public class IncorrectArgumentsClass : IRepository
    {
        private readonly IList<int> _list;

        public IncorrectArgumentsClass(int first, int second)
        {
            _list = new List<int> { first, second };
        }

        public string Foo()
        {
            return string.Join(",", _list);
        }
    }
}
