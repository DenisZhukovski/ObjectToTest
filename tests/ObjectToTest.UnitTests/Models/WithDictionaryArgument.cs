using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectToTest.UnitTests.Models
{
    internal class WithDictionaryArgument
    {
        private readonly Dictionary<int, string> _dictionary;

        public WithDictionaryArgument(Dictionary<int, string> dictionary)
        {
            _dictionary = dictionary;
        }
    }
}
