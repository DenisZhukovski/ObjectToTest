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
                "new WithUserArgument(null,new WithUserPublicProperty(){User = null})",
                withUser.ToTest()
            );
        }

        [Fact]
        public void NoSharedArgumentsInit()
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
        
        [Fact(Skip = "Need to fix this test")]
        public void CircularReferenceInitProperties()
        {
            /*
             * @todo #11:60m/DEV Make CircularReferenceInitProperties test to be green.
             * Now the circular references not initialized properly and does not detect that shared object
             * has circular reference in its fields or properties.
             * It would nice to fix the test.
             */
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.Equal(
                $"var circularRefPublicProperty2 = new CircularRefPublicProperty2();{Environment.NewLine}" +
                $"var circularRefPublicProperty1 = new CircularRefPublicProperty1();{Environment.NewLine}" +
                $"circularRefPublicProperty2.PropertyName1 = circularRefPublicProperty1;{Environment.NewLine}" +
                $"circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;",
                new ObjectSharedArguments(o1).ToString().Log(_output)
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
    }
}

