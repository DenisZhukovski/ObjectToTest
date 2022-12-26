using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class LiteralAwareSubstringTests
    {
        [Fact]
        public void NewLiteralForArray()
        {
            Assert.Equal(
                new Substring("new", 0, 2),
                new LiteralAwareSubstring(
                    "new[] { 1, 2, 4, 5 }",
                    "new"
                ).First() 
           );
        }
    }
}