using System.Linq;
using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
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
            InstantiationStatement
                .Parse("new Foo()")
                .ClaimIs<ParseSuccessful>();
        }

        [Fact]
        public void Fail_Literal()
        {
            InstantiationStatement
                .Parse("\"new Foo()\"")
                .ClaimIs<ParseFail>();
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
        public void AnonymousArrayType()
        {
            new InstantiationStatement("new[] { 1, 2, 4, 5 }")
                .Type.ToString()
                .ClaimEqual("[]");
        }

        [Fact]
        public void Success_AnonymousArrayType()
        {
            var arr = InstantiationStatement.Parse("new[] { 1, 2, 4, 5 }");
            arr.ClaimIs<ParseSuccessful<IInstantiationStatement>>();

            var instantiationStatement = ((ParseSuccessful<IInstantiationStatement>) arr).Value;

            instantiationStatement.Type.ToString().ClaimEqual("[]");
            instantiationStatement.Arguments.ToString().ClaimEqual(string.Empty);
            instantiationStatement.InlinePropertiesAssignment.Count().ClaimEqual(4);
        }

        [Fact]
        public void InstantiationShouldBeParsedCorrectly()
        {
            var statement = new InstantiationStatement("new Foo(123, \"new Foo()\", new[] {1, 2, 3}) {A = 5, B = \"new Foo()\"};");
            statement.Type.ToString().ClaimEqual("Foo");
            statement.Arguments.ToString().ClaimEqual("123, \"new Foo()\", new[] {1, 2, 3}");
            statement.Arguments.ElementAt(2).ToString().ClaimEqual("new[] {1, 2, 3}");
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