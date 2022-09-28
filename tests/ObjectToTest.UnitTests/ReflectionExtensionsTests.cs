using System;
using System.Collections.Generic;
using ObjectToTest.UnitTests.Models;
using UnityEngine;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void StructValues()
        {
            Assert.Equal(
                new List<object> { 99 },
                new WithIndexator(99).Values()
            );
        }
    }
}
