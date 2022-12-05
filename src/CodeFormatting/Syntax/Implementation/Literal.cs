using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Common.Parse;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class Literal : IArgument
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
                return new ParseSuccessful<IArgument>(new Literal(codeStatement));
            }

            return new ParseFail();
        }

        public override string ToString()
        {
            return _codeStatement.Trim();
        }
    }
}