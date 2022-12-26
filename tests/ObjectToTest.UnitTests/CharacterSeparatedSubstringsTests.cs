using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class CharacterSeparatedSubstringsTests
    {
        [Fact]
        public void ArgumentsCount()
        {
            Assert.Equal(
                2,
                new CharacterSeparatedSubstrings(
                    "18,5",
                    ',',
                    notAnalyzeIn: new LiteralsAndClosuresSubstrings("18,5")
                ).Count()
            );
        }
    }
}