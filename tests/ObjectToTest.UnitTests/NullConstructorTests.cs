using ObjectToTest.Constructors;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class NullConstructorTests
    {
        [Fact]
        public void NullType()
        {
            Assert.Equal(
                "null",
                new NullConstructor().Type
            );
        }
        
        [Fact]
        public void NullObject()
        {
            Assert.Null(
                new NullConstructor().Object
            );
        }
        
        [Fact]
        public void EmptyArguments()
        {
            Assert.Empty(
                new NullConstructor().Arguments
            );
        }
    }
}