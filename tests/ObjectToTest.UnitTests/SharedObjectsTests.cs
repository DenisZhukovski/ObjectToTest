using System;
using System.Collections.Generic;
using ObjectToTest.UnitTests.Data;
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
            Assert.Empty(
                new SharedObjects(Vector3.forward).ToList()
            );
        }
        
        [Fact]
        public void SingletonNotSharedArgument()
        {
            Assert.Empty(
                new SharedObjects(new WithSingletonArgument(SingletonClass.Instance)).ToList()
            );
        }
        
        [Fact]
        public void SameEqualSameHashCode()
        {
            var customHashCode = new WithCustomHashCode("11", 1);
            Assert.NotEmpty(
                new SharedObjects(
                    new WithCustomDataExtended(
                        new WithCustomData(customHashCode),
                        customHashCode
                    )
                ).ToList()
            );
        }

        [Fact]
        public void SharedAsMethodDelegate()
        {
            var user = new User("user name");
            Assert.Equal(
                2,
                new SharedObjects(
                    new With2FuncArguments(user.Age, user.LoginToAsync)
                ).ToList().Count
            );
        }

        [Fact]
        public void NotSharedDelegateMethod()
        {
            Assert.Empty(
                new SharedObjects(
                    new WithActionArgument((pos) => { })
                ).ToList()
            );
        }
    }
}