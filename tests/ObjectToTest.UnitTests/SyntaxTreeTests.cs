using System.Linq;
using ObjectToTest.CodeFormatting.Syntax;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
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
                ).Count()
            );
        }

        [Fact]
        public void SingleLineWithoutSemicolon()
        {
            Assert.Single(
                new SyntaxTree("first")
            );
        }

        [Fact]
        public void InstantiationShouldBeDetectedCorrectly()
        {
            new SyntaxTree("new Foo()")
                .ElementAt(0)
                .ClaimIs<IInstantiationStatement>();
        }

        [Fact]
        public void InstantiationInLiteralShouldBeIgnored()
        {
            new SyntaxTree("\"new Foo()\"")
                .ElementAt(0)
                .ClaimIsNot<IInstantiationStatement>();
        }
    }
}