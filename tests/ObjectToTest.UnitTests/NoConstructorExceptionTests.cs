using ObjectToTest.Exceptions;
using ObjectToTest.UnitTests.Extensions;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class NoConstructorExceptionTests
    {
        [Fact]
        public void Deserialization()
        {
            Assert.NotNull(
                new NoConstructorException(typeof(WithClassParam)).Deserialize()
            );
        }
    }
}

