using System;
using ObjectToTest.Exceptions;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ObjectSharedArgumentsTests
    {
        [Fact(Skip = "Need to fix this test")]
        /**
           * @todo #12:60m/DEV Improve the test: Ideally the test should compare the result that returned by Argument method
           * with some manually created argument that wraps the original object.
           * Assert.Equal(
           *    new Argument(user),
           *    new ObjectSharedArguments(withUser).Argument(user)
           * );
           */
        public void SharedArgument()
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
    }
}

