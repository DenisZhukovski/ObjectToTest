using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation;
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
        
        [Fact(Skip = "Need to be fixed in scope of puzzle #156")]
        public void AnonymousArrayType()
        {
            /*
            * @todo #156 60m/DEV InstantiationStatement is no capable to recognize anonymous array
             * instantiation. Need to be fixed
            */
            new InstantiationStatement("new[] { 1, 2, 4, 5 }")
                .Type.ToString()
                .ClaimEqual(new[] {1,2, 4}.GetType().Name);
        }

        [Fact]
        public void InstantiationShouldBeParsedCorrectly()
        {
            var statement = new InstantiationStatement("new Foo(123, \"new Foo()\") {A = 5, B = \"new Foo()\"};");
            statement.Type.ToString().ClaimEqual("Foo");
            statement.Arguments.ToString().ClaimEqual("123, \"new Foo()\"");
            statement.InlinePropertiesAssignment.ToString().ClaimEqual("A = 5, B = \"new Foo()\"");
            statement.InlinePropertiesAssignment.ElementAt(0).ToString().ClaimEqual("A = 5");
            statement.InlinePropertiesAssignment.ElementAt(1).ToString().ClaimEqual("B = \"new Foo()\"");
        }

        [Fact]
        public void InstantiationStatementWithLastShortConstant()
        {
            new InstantiationStatement("new TimeSpan(18,17,34,24,5)")
                .Arguments
                .Count().ClaimEqual(5);
        }
    }
}