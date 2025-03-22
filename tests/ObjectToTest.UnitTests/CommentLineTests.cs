using ObjectToTest.Constructors;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class CommentLineTests(ITestOutputHelper output)
    {
        [Fact]
        public void AsComment()
        {
            Assert.Equal(
                "// Its a comment",
                new CommentLine("Its a comment")
                    .ToString()
                    .Log(output)
            );
        }
        
        [Fact]
        public void AsValidConstructor()
        {
            Assert.True(
                (new CommentLine("Its a comment") as IConstructor).IsValid
            );
        }
        
        [Fact]
        public void NoConstructorArguments()
        {
            Assert.Empty(
                (new CommentLine("Its a comment") as IConstructor).Arguments
            );
        }
    }
}