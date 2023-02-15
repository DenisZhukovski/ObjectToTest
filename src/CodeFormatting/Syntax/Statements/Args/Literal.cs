using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Args
{
    public class Literal : ILiteral
    {
        private readonly string _codeStatement;

        private Literal(string codeStatement)
        {
            _codeStatement = codeStatement;
        }

        public static ParseResult Parse(string codeStatement)
        {
            var isInstantiation = new LiteralSubstrings(codeStatement).Any();

            if (isInstantiation)
            {
                return new ParseSuccessful<ILiteral>(new Literal(codeStatement));
            }

            return new ParseFail();
        }

        public override string ToString()
        {
            return _codeStatement.Trim();
        }
    }
}