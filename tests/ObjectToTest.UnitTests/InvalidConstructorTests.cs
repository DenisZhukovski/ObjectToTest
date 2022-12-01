using ObjectToTest.Constructors;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class InvalidConstructorTests
    {
        [Fact]
        public void ArgumentsNotEmptyWhenInvalid()
        {
            Assert.Equal(
                2,
                new InvalidConstructor(
                    new ComplexObjectWithInvalidArguments(
                        new InvalidDependencyUser(new Price(20), new IncorrectArgumentsClass(1, 2)),
                        new Price(30)
                    )
                ).Arguments.Count
            );
        }
        
        
        [Fact]
        public void ArgumentsNotEmptyWhenNotFound()
        {
            Assert.Equal(
                2,
                new InvalidConstructor(
                    new IncorrectArgumentsClass(1, 2)
                ).Arguments.Count
            );
        }
    }
}