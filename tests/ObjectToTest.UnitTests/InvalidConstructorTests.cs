using System.Collections.Generic;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class InvalidConstructorTests
    {
        [Fact(Skip = "Need to fix this test")]
        public void ArgumentsNotEmpty()
        {
            /*
             * @todo #64 60m/DEV InvalidConstructor returns Arguments as empty list
             * The class should be able to calculate the target object constructor
             * and return the list of its arguments
            */
            Assert.Equal(
                new List<IArgument>
                {
                    new Argument(
                        "user",
                        new InvalidDependencyUser(new Price(20), new IncorrectArgumentsClass(1, 2))
                    ),
                    new Argument("price", new Price(30))
                },
                new InvalidConstructor(
                    new ComplexObjectWithInvalidArguments(
                        new InvalidDependencyUser(new Price(20), new IncorrectArgumentsClass(1, 2)),
                        new Price(30)
                    )
                ).Arguments
            );
        }
    }
}