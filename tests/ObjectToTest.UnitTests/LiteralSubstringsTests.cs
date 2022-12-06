using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
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
    }
}