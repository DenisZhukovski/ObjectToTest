using System;
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
    }
}