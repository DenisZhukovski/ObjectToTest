using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class LiteralSubstringsTests
    {
        [Fact]
        public void AllLiteralsShouldBeDetected()
        {
            new LiteralSubstrings("new Foo(\"Test\", \"Test2\", \")")
                .Select(x => x.ToString())
                .ClaimCollectionEqual("\"Test\"", "\"Test2\"");
        }
        
        [Fact]
        public void NoPatternFoundReturnsEmptyString()
        {
            Assert.Equal(
                new Substring(string.Empty),
                new LiteralSubstrings(
                    "new[] { 1, 2, 4, 5 }"
                ).First() 
            );
        }
    }
}
