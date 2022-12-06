using System;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class PossibleItemsRelyOnParseMethodTests
    {
        [Fact]
        public void FailIfNoWildcard()
        {
            Assert.Throws<InvalidOperationException>(() => new PossibleItems<int>(TryParse).BestMatch("42"));
        }

        [Fact]
        public void PropagateToWildcardIfFailed()
        {
            Assert.Equal(
                42,
                new PossibleItems<int>(
                    TryParse,
                    value => new ParseSuccessful<int>(42)
                ).BestMatch("undefined")
            );
        }

        [Fact]
        public void ParseShouldBeSuccessful()
        {
            Assert.Equal(
                1,
                new PossibleItems<int>(
                    TryParse,
                    value => new ParseSuccessful<int>(42)
                ).BestMatch("1")
            );
        }

        private static ParseResult TryParse(string value)
        {
            if (value == "1")
            {
                return new ParseSuccessful<int>(1);
            }

            return new ParseFail();
        }
    }
}