using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectToTest.UnitTests.Models
{
    internal class WithListArgument
    {
        private readonly List<int> _listArgument;

        public WithListArgument(List<int> listArgument)
        {
            _listArgument = listArgument;
        }
    }
}
