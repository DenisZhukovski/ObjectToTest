using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SubstringBetweenTests
    {
        [Fact]
        public void EmptyStringWhenNoPatternFound()
        {
            Assert.Equal(
                string.Empty,
                new SubstringBetween("new[] { 1, 2, 4, 5 }", "new", "(").ToString()
            );
        }
    }
}