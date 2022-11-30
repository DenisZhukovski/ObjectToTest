using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ArgumentTests
    {
        [Fact]
        public void NullArgumentConstructor()
        {
            Assert.Equal(
                typeof(NullConstructor),
                new Argument("test", null).Constructor.GetType()
            );
        }
    }
}