using System;
using ObjectToTest.Arguments;
using ObjectToTest.Exceptions;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ObjectSharedArgumentsTests
    {
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
                new Argument("user", user.ValidConstructor(new MockArguments())),
                new ObjectSharedArguments(withUser).Argument(user)
            );
        }

        [Fact(Skip = "Failing need to fix")]
        /**
           * @todo #12:60m/DEV ObjectSharedArguments should be able to support nulls for shared objects.
           */
        public void SharedNullArgument()
        {
            User user = null;
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.NotNull(
                new ObjectSharedArguments(withUser).Argument(user)
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

