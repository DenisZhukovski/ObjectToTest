using System.Collections.Generic;

namespace ObjectToTest.UnitTests.Models
{
    internal class WithListArgument
    {
        private readonly List<int> _listArgument;

        public WithListArgument(List<int> listArgument)
        {
            _listArgument = listArgument;
        }

        public List<int> ToList()
        {
            return _listArgument;
        }
    }
}
