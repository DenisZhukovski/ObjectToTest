using ObjectToTest.Constructors;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class CommentLineTests
    {
        private readonly ITestOutputHelper _output;

        public CommentLineTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void AsComment()
        {
            Assert.Equal(
                    "// Its a comment",
                new CommentLine("Its a comment")
                        .ToString()
                        .Log(_output)
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