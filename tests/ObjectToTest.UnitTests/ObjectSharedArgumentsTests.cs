using System;
using ObjectToTest.Exceptions;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ObjectSharedArgumentsTests
    {
        [Fact]
        /**
           * @todo #12:60m/DEV Improve the test: Ideally the test should compare the result that returned by Argument method
           * with some manually created argument that wraps the original object.
           * Assert.Equal(
           *    new Argument(user),
           *    new ObjectSharedArguments(withUser).Argument(user)
           * );
           */
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
            Assert.NotNull(
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

        [Fact(Skip = "Failing need to fix")]
        public void SharedArgumentsInit()
        {
            /**
            * @todo #12:60m/DEV ObjectSharedArguments class should return as a string the piepce
            * of code that initializes the shared object arguments. For example:
            * var user = new User("user name");
            * var withUser = new WithUserArgument(
            *   user,
            *   new WithUserPublicProperty
            *   {
            *       User = user
            *    }
            * );
            * 
            * ToString method should return:
            * var user = new User("user name");
            * 
            * If object has no shared objects in its internal state the method should return empty string.
            */

            User user = null;
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                "var user = new User(\"user name\");",
                new ObjectSharedArguments(withUser).ToString()
            );
        }
    }
}

