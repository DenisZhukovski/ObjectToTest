using System.Linq;
using ObjectToTest.CodeFormatting.Syntax;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.UnitTests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class SyntaxTreeTests
    {
        private readonly ITestOutputHelper _output;

        public SyntaxTreeTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
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
        
        [Fact(Skip = "Need to be fixed in scope of puzzle")]
        public void SyntaxTreeHierarchyReport()
        {
            /*
             * @todo #:60m/ARCH SyntaxTree ToString should show the syntax hierarchy.
             * Something similar to the example 'new TimeSpan(18, 17, 34, 24, 5)':
             * - InstantiationStatement: TimeSpan
             *   - Arguments: 5
             *      - Argument: Int32(18)
             *      - Argument: Int32(17)
             *      - Argument: Int32(34)
             *      - Argument: Int32(24)
             *      - Argument: Int32(5)
            */
            Assert.Equal(
                new NewLineSeparatedString(
                    "- InstantiationStatement: TimeSpan",
                    "  - Arguments: 5",
                    "      - Argument: Int32(18)",
                    "      - Argument: Int32(17)",
                    "      - Argument: Int32(34)",
                    "      - Argument: Int32(24)",
                    "      - Argument: Int32(5)"
                ).ToString(),
                new SyntaxTree(
                        "new TimeSpan(18, 17, 34, 24, 5)"
                ).ToString(_output)
            );
        }

        [Fact]
        public void SyntaxTreeHierarchyReportNotCrash()
        {
            Assert.NotEmpty(
                new SyntaxTree(
                "new TimeSpan(18, 17, 34, 24, 5)"
                ).ToString(_output)
            );  
        }
    }
}