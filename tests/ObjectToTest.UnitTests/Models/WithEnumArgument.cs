using System;
using UnityEngine;

namespace ObjectToTest.UnitTests.Models
{
    public class WithEnumArgument
    {
        private readonly FlaggedEnum _position;

        public WithEnumArgument(FlaggedEnum position)
        {
            _position = position;
        }

        public string Hello()
        {
            return _position.ToString();
        }
    }
}
