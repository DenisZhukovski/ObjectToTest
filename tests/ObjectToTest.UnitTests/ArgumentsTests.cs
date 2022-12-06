using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ArgumentsTests
    {
        [Fact]
        public void ArgumentsShouldBeParsedCorrectly()
        {
            var arguments = new CodeFormatting.Syntax.Statements.Args.Arguments("123, \"new Foo()\", new Foo()");
            arguments.ElementAt(0).ToString().ClaimEqual("123");
            arguments.ElementAt(1).ToString().ClaimEqual("\"new Foo()\"");
            arguments.ElementAt(2).ClaimIs<IInstantiationStatement>();
            arguments.ElementAt(2).ToString().ClaimEqual("new Foo()");
        }

        [Fact]
        public void InnerArgumentsShouldBeIgnored()
        {
            var arguments = new CodeFormatting.Syntax.Statements.Args.Arguments("new Foo(123, \"new Foo()\")");
            arguments.ElementAt(0).ClaimIs<IInstantiationStatement>();
            arguments.ElementAt(0).ToString().ClaimEqual("new Foo(123, \"new Foo()\")");
        }
    }
}