using System;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SharedArgumentTests
    {
        [Fact]
        public void ArgumentEquals()
        {
            var user = new User("user");
            var argument = new Argument("user", user.ValidConstructor(new MockArguments()));
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
            var argument = new Argument("user", user.ValidConstructor(new MockArguments()));
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
            var argument = new Argument("user", user.ValidConstructor(new MockArguments()));
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
            var argument = new Argument("user", user.ValidConstructor(new MockArguments()));
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
            Assert.Equal(
                "var user = new User(\"user\")",
                new SharedArgument(
                        new Argument(
                            "user",
                            new User("user").ValidConstructor(new MockArguments())
                        )
                 ).ToString()
            );
        }

        [Fact]
        public void AsFieldNameString()
        {
            var argument = new SharedArgument(
                   new Argument(
                       "user",
                       new User("user").ValidConstructor(new MockArguments())
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

