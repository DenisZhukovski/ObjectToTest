using ObjectToTest.UnitTests.Data;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class ObjectAsConstructorTests
    {
        private readonly ITestOutputHelper _output;

        public ObjectAsConstructorTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ObjectWithDecimalConstructor()
        {
            Assert.Equal(
                "new Price(10)",
                new Price(10).ToXUnit()
            );
        }

        [Fact]
        public void NoConstructor_WithDefault()
        {
            Assert.Equal(
                "new NoConstructor()",
                new NoConstructor().ToXUnit()
            );
        }

        [Fact]
        public void WithGenericArgument()
        {
            Assert.Equal(
                "new WithGenericArgument<IPrice>()",
                new WithGenericArgument<IPrice>().ToXUnit()
            );
        }
    }
}
