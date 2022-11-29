using ObjectToTest.Arguments;
using ObjectToTest.UnitTests.Data;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SharedArgumentTests
    {
        [Fact]
        public void ArgumentEquals()
        {
            var user = new User("user");
            var argument = new Argument(
                "user",
                user.GetType(),
                user,
                user.Constructor(new MockArguments())
            );
            Assert.Equal<object>(
                argument,
                new SharedArgument(
                        argument
                 )
            );
        }

        [Fact]
        public void Name()
        {
            var user = new User("user");
            var argument = new Argument("user", user.GetType(), user, user.Constructor(new MockArguments()));
            Assert.Equal(
                argument.Name,
                new SharedArgument(
                        argument
                 ).Name
            );
        }

        [Fact]
        public void Constructor()
        {
            var user = new User("user");
            var argument = new Argument("user", user.GetType(), user, user.Constructor(new MockArguments()));
            Assert.Equal(
                argument.Constructor,
                new SharedArgument(
                        argument
                 ).Constructor
            );
        }

        [Fact]
        public void HashCode()
        {
            var user = new User("user");
            var argument = new Argument("user", user.GetType(), user, user.Constructor(new MockArguments()));
            Assert.Equal(
                argument.GetHashCode(),
                new SharedArgument(
                        argument
                 ).GetHashCode()
            );
        }

        [Fact]
        public void AsInitString()
        {
            var user = new User("user");
            Assert.Equal(
                "var user = new User(\"user\")",
                new SharedArgument(
                        new Argument(
                            "user",
                            user.GetType(),
                            user,
                            user.Constructor(new MockArguments())
                        )
                 ).ToString()
            );
        }

        [Fact]
        public void AsFieldNameString()
        {
            var user = new User("user");
            var argument = new SharedArgument(
                   new Argument(
                       "user",
                       user.GetType(),
                       user,
                       user.Constructor(new MockArguments())
                   )
            );

            argument.ToString(); // simulate init
            Assert.Equal(
                "user",
                argument.ToString()
            );
        }
    }
}

