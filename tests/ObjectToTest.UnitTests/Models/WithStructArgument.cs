using System;
using UnityEngine;

namespace ObjectToTest.UnitTests.Models
{
    public class WithStructArgument
    {
        private readonly Vector3 position;

        public WithStructArgument(Vector3 position)
        {
            this.position = position;
        }

        public string Hello()
        {
            return this.position.ToString();
        }
    }
}
