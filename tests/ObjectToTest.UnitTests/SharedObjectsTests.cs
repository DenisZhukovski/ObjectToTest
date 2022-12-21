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
                new List<object>{ o1, o2 },
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
        public void SimpleObjectWithPropertyNotShared()
        {
            Assert.Empty(
                new SharedObjects(
                    new WithUserPublicProperty
                    {
                        User = new User("user")
                    }
                ).ToList()
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

        [Fact]
        public void ChildrenOfSharedConsideredAsNotShared()
        {
            var customUser = new CustomUserWithDependency(new User("user name"));
            var withUser = new WithUserArgument(
                customUser,
                new WithUserPublicProperty
                {
                    User = customUser
                }
            );

            Assert.Single(
                new SharedObjects(
                    withUser
                ).ToList()
            );
        }

        [Fact]
        public void SeveralTimesCtorArgumentAsShared()
        {
            var user = new User("user name");
            Assert.Equal(
                1,
                new SharedObjects(
                    new With2ObjectArguments(user, user)
                ).ToList().Count
            );
        }
    }
}