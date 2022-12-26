using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class LiteralAwareClosureSubstringsTests
    {
        [Fact]
        public void ArgumentsClosure()
        {
            Assert.True(
                new LiteralAwareClosureSubstrings(
                    "new TimeSpan(18,17,34,24,5)",
                    '(',
                    ')'
                ).Any()
            );
        }
    }
}