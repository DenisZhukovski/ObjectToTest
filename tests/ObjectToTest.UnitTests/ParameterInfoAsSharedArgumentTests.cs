using System;
using System.Linq;
using System.Reflection;
using ObjectToTest.Arguments;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ParameterInfoAsSharedArgumentTests
    {
        private static readonly ConstructorInfo _userConstructor = new CustomUserWithDependency(
                new User("user name")
        ).GetType().GetConstructors().First();
        
        [Fact]
        public void ArgumentEquals()
        {
            var argument = new Argument("user", new User("user"));
            Assert.Equal<object>(
                argument,
                new ParameterInfoAsSharedArgument(
                    argument,
                    _userConstructor.GetParameters().First()
                )
            );
        }

        [Fact]
        public void Name()
        {
            var argument = new Argument("user", new User("user"));
            Assert.Equal(
                argument.Name,
                new ParameterInfoAsSharedArgument(
                    argument,
                    _userConstructor.GetParameters().First()
                ).Name
            );
        }
        
        [Fact]
        public void Type()
        {
            Assert.Equal(
                typeof(IUser).TypeName(),
                new ParameterInfoAsSharedArgument(
                    new Argument("user", new User("user")),
                    _userConstructor.GetParameters().First()
                ).Type
            );
        }

        [Fact]
        public void Constructor()
        {
            var argument = new Argument("user", new User("user"));
            Assert.Equal(
                argument.Constructor,
                new ParameterInfoAsSharedArgument(
                    argument,
                    _userConstructor.GetParameters().First()
                ).Constructor
            );
        }

        [Fact]
        public void HashCode()
        {
            var argument = new Argument("user", new User("user"));
            Assert.Equal(
                argument.GetHashCode(),
                new ParameterInfoAsSharedArgument(
                    argument,
                    _userConstructor.GetParameters().First()
                ).GetHashCode()
            );
        }

        [Fact]
        public void AsInitString()
        {
            var user = new User("user");
            Assert.Equal(
                "new User(\"user\")",
                new ParameterInfoAsSharedArgument(
                    new Argument(
                        "user",
                        user
                    ),
                    _userConstructor.GetParameters().First()
                ).ToString()
            );
        }

        [Fact]
        public void AsFieldNameString()
        {
            var argument = new ParameterInfoAsSharedArgument(
                   new Argument(
                       "user",
                       new User("user")
                   ),
                   _userConstructor.GetParameters().First()
            );

            argument.ToString(); // simulate init
            Assert.Equal(
                "new User(\"user\")",
                argument.ToString()
            );
        }
    }
}

