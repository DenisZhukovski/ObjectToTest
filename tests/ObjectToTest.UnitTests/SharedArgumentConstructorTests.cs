using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.UnitTests.Data;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SharedArgumentConstructorTests
    {
        [Fact]
        public void TheSameObject()
        {
            var user = new User("test user");
            Assert.Equal(
                user,
                new SharedArgumentConstructor(
                    new SharedArgument(
                        new Argument("user", user)
                    )
                ).Object
            );
        }
    }
}