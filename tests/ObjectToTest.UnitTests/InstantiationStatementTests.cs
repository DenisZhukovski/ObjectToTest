using ObjectToTest.CodeFormatting.Syntax.Common.Parse;
using ObjectToTest.CodeFormatting.Syntax.Implementation;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class InstantiationStatementTests
    {
        [Fact]
        public void Success()
        {
            InstantiationStatement.Parse("new Foo()").ClaimIs<ParseSuccessful>();
        }

        [Fact]
        public void Fail_Literal()
        {
            InstantiationStatement.Parse("\"new Foo()\"").ClaimIs<ParseFail>();
        }

        [Fact]
        public void Fail_PartOfMethod()
        {
            InstantiationStatement.Parse("foo.new()").ClaimIs<ParseFail>();
        }

        [Fact]
        public void Fail_PartOfClassName()
        {
            InstantiationStatement.Parse("newFoo.Foo()").ClaimIs<ParseFail>();
        }

        [Fact]
        public void Success_PartOfAssignment()
        {
            InstantiationStatement.Parse("=new Foo.Foo()").ClaimIs<ParseSuccessful>();
        }

        [Fact]
        public void InstantiationShouldBeParsedCorrectly()
        {
            var statement = new InstantiationStatement("new Foo(123, \"new Foo()\") {A = 5, B = \"new Foo()\"};");
            statement.Type.ToString().ClaimEqual("Foo");
            statement.Arguments.ToString().ClaimEqual("123, \"new Foo()\"");
            statement.InlinePropertiesAssignment.ToString().ClaimEqual("A = 5, B = \"new Foo()\"");
        }
    }
}