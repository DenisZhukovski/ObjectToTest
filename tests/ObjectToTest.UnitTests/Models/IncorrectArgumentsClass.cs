using System;
using System.Collections.Generic;

namespace ObjectToTest.UnitTests.Models
{
    public class IncorrectArgumentsClass
    {
        private IList<int> _list;

        public IncorrectArgumentsClass(int first, int second)
        {
            _list = new List<int> { first, second };
        }
    }
}
