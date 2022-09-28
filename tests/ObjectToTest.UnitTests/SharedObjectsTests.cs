using System.Collections.Generic;
using ObjectToTest.UnitTests.Models;
using UnityEngine;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SharedObjectsTests
    {
        [Fact]
        public void CircularReferenceDetection()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.Equal(
                new List<object>{ o2, o1 },
                new SharedObjects(o1).ToList()
            );
        }

        [Fact]
        public void VectorSharedObjects()
        {
            Assert.Equal(
                new List<object>(),
                new SharedObjects(Vector3.forward).ToList()
            );
        }
    }
}