using System;
using ObjectToTest.Constructors;
using UnityEngine;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ValueTypeConstructorTests
    {
        [Fact]
        public void StructCtor()
        {
            Assert.Equal(
                "new Vector3(0,0,1)",
                new ValueTypeConstructor(Vector3.forward).ToString()
            );
        }

        [Fact]
        public void TimeSpanCtor()
        {
            Assert.Equal(
               "new TimeSpan(18,17,34,24,5)",
               new ValueTypeConstructor(new TimeSpan(18, 17, 34, 24, 5)).ToString()
           );
        }
    }
}
