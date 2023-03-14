using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using UnityEngine;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class SharedObjectsTests
    {
        private readonly ITestOutputHelper _output;

        public SharedObjectsTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void CircularReferenceDetection()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2 { PropertyName1 = o1 };
            o1.PropertyName = o2;
            Assert.Equal(
                new List<object>{ o1, o2 },
                new SharedObjects(o1, true).ToList()
            );
        }

        [Fact]
        public void VectorSharedObjects()
        {
            Assert.Empty(
                new SharedObjects(Vector3.forward, true).ToList()
            );
        }
        
        [Fact]
        public void SingletonNotSharedArgument()
        {
            Assert.Empty(
                new SharedObjects(
                    new WithSingletonArgument(SingletonClass.Instance),
                    true
                ).ToList()
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
                    }, 
                    true
                ).ToList()
            );
        }
        
        [Fact]
        // Not Clear what this test is about????
        public void SameEqualSameHashCode()
        {
            var customHashCode = new WithCustomHashCode("11", 1);
            Assert.NotEmpty(
                new SharedObjects(
                    new WithCustomDataExtended(
                        new WithCustomData(customHashCode),
                        customHashCode
                    ),
                    true
                ).ToList()
            );
        }

        [Fact]
        public void SharedAsMethodDelegate()
        {
            var user = new User("user name");
            Assert.Equal(
                3,
                new SharedObjects(
                    new With2FuncArguments(user.Age, user.LoginToAsync),
                    true
                ).ToList().Count
            );
        }

        [Fact]
        public void NotSharedDelegateMethod()
        {
            Assert.Empty(
                new SharedObjects(
                    new WithActionArgument((pos) => { }),
                    true
                ).ToList()
            );
        }

        [Fact]
        public void ChildrenOfSharedConsideredAsNotShared()
        {
            var customUser = new CustomUserWithDependency(new User("user name"));
            Assert.Single(
                new SharedObjects(
                    new WithUserArgument(
                        customUser,
                        new WithUserPublicProperty
                        {
                            User = customUser
                        }
                    ),
                    true
                ).ToList()
            );
        }

        [Fact]
        public void SeveralTimesCtorArgumentAsShared()
        {
            var user = new User("user name");
            Assert.Single(
                new SharedObjects(
                    new With2ObjectArguments(user, user),
                    true
                ).ToList()
            );
        }

        [Fact]
        public void HttpClientHasNoSharedArguments()
        {
            Assert.Empty(
                new SharedObjects(new HttpClient(), false).ToList()
            );
        }

        [Fact]
        public void InitSharedAsMethodDelegate()
        {
            var user = new User("user name");
            Assert.Equal(
                new List<object>
                {
                    user,
                    new Func<int>(user.Age),
                    new Func<Task>(user.LoginToAsync)
                },
                new With2FuncArguments(user.Age, user.LoginToAsync)
                    .SharedObjects(true)
                    .Log(_output)
            );
        }
        
    }
}