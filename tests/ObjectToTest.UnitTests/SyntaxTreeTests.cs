using System.Linq;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Extensions;
using ObjectToTest.UnitTests.Extensions;
using Xunit;
using Xunit.Abstractions;
using SyntaxTreeDump = ObjectToTest.CodeFormatting.Syntax.Dump.SyntaxTreeDump;

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
        
        [Fact]
        public void SyntaxTreeHierarchyReport()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                    "InstantiationStatement",
                    "    Type: TimeSpan",
                    "    Arguments: 5",
                    "        Literal: 18",
                    "        Literal: 17",
                    "        Literal: 34",
                    "        Literal: 24",
                    "        Literal: 5",
                    "    EmptyInlineAssignment: 0"
                ).ToString(),
                new SyntaxTree(
                        "new TimeSpan(18, 17, 34, 24, 5)"
                ).Dump().Log(_output)
            );
        }

        [Fact]
        public void SyntaxTreeHierarchyReportNotCrash()
        {
            Assert.NotEmpty(
                new SyntaxTree(
                "new TimeSpan(18, 17, 34, 24, 5)"
                ).Dump().Log(_output)
            );  
        }

        [Fact]
        public void CtorWithComplexArgumentsAndProperties()
        {
            new SyntaxTree(
                    new NewLineSeparatedString(
                        "new WithClassParamWithProp(",
                        "    new WithOnePublicProperty()",
                        "    {",
                        "        PropertyName = \"Test\"",
                        "    },",
                        "    42",
                        ")"
                    ).ToString()
                ).Dump().Log(_output)
                .ClaimEqual(
                    new NewLineSeparatedString(
                        "InstantiationStatement",
                        "    Type: WithClassParamWithProp",
                        "    Arguments: 2",
                        "        InstantiationStatement",
                        "            Type: WithOnePublicProperty",
                        "            Arguments: 0",
                        "            InlineAssignments: 1",
                        "                Assignment",
                        "                    Left Part: PropertyName",
                        "                    Value:",
                        "                        Literal: \"Test\"",
                        "        Literal: 42",
                        "    EmptyInlineAssignment: 0"
                    ).ToString()
                );
        }
    }
}