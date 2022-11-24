using System;
using ObjectToTest.Arguments;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class ObjectSharedArgumentsTests
    {
        private readonly ITestOutputHelper _output;

        public ObjectSharedArgumentsTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void SharedNotNullArgument()
        {
            var user = new User("user name");
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                new Argument("user", user, user.ValidConstructor(new MockArguments())),
                new ObjectSharedArguments(withUser).Argument(user)
            );
        }

        [Fact]
        public void SharedNullArgument()
        {
            User? user = null;
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                "new WithUserArgument(null,new WithUserPublicProperty())",
                withUser.ToTest()
            );
        }

        [Fact]
        public void EmptyStringWhenNoSharedArguments()
        {
            var withUser = new WithUserPublicProperty
            {
                User = new User("user")
            };
            Assert.Empty(
                new ObjectSharedArguments(withUser).ToString()
            );
        }

        [Fact]
        public void SharedArgumentsInit()
        {
            User user = new User("user name");
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}",
                new ObjectSharedArguments(withUser).ToString()
            );
        }
        
        [Fact]
        public void CircularReferenceInitProperties()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.Equal(
                $"var circularRefPublicProperty2 = new CircularRefPublicProperty2();{Environment.NewLine}" +
                $"var circularRefPublicProperty1 = new CircularRefPublicProperty1();{Environment.NewLine}" +
                $"circularRefPublicProperty2.PropertyName1 = circularRefPublicProperty1;{Environment.NewLine}" +
                $"circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;{Environment.NewLine}",
                
                new SharedCircularProperties(
                    new ObjectSharedArguments(o1)
                ).ToString().Log(_output)
            );
        }

        [Fact]
        public void SharedArgumentsInitProperties()
        {
            User user = new User("user name");
            var with2PublicProperties = new With2PublicProperties
            {
                User = user,
                UserPublicProperty = new WithUserPublicProperty
                {
                    User = user
                }
            };
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}",
                new ObjectSharedArguments(with2PublicProperties).ToString()
            );
        }

        [Fact]
        public void SingletonNotSharedArgument()
        {
            Assert.Empty(
                new ObjectSharedArguments(new WithSingletonArgument(SingletonClass.Instance)).ToList()
            );
        }

        [Fact]
        public void SameEqualSameHashCode()
        {
            var customHashCode = new WithCustomHashCode("11", 1);
            Assert.Equal(
                "var customHashCode = new WithCustomHashCode(\"11\", 1)",
                new ObjectSharedArguments(
                    new WithCustomDataExtended(
                        new WithCustomData(customHashCode),
                        customHashCode
                    )
                ).ToString().Log(_output)
            );
        }

        //public void SameEqualButDifferentHashCode()
        //{
        //    var customHashCode = new WithCustomHashCode("11", 1);
        //    Assert.Equal(
        //        new WithCustomDataExtended(
        //            new WithCustomData(customHashCode),
        //            customHashCode
        //        ).ToTest()
        //    );
        //}
    }
}

