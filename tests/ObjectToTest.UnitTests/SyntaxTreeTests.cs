using System.Linq;
using ObjectToTest.CodeFormatting.Syntax;
using ObjectToTest.CodeFormatting.Syntax.Implementation;
using ObjectToTest.UnitTests.Extensions;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SyntaxTreeTests
    {
        [Fact]
        public void NumberOfCodeParts()
        {
            Assert.Equal(
                3,
                new SyntaxTree(
                        new NewLineSeparatedString(
                            "first; something second;",
                            "third;"
                        ).ToString()
                    )
                    .Count()
            );
        }

        [Fact]
        public void SingleLineWithoutSemicolon()
        {
            Assert.Equal(
                1,
                new SyntaxTree(
                        "first"
                    )
                    .Count()
            );
        }
    }
}