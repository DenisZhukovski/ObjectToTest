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
            Assert.True(
                argument.Equals(
                    new SharedArgument(
                        argument
                    )
                )
            );
        }

        //public string Name => _argument.Name;

        //public IConstructor Constructor => _constructor ??= new SharedArgumentConstructor(_argument);

        //public override bool Equals(object? obj)
        //{
        //    return _argument.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return _argument.GetHashCode();
        //}

        //public override string ToString()
        //{
        //    return Constructor.ToString();
        //}
    }
}

