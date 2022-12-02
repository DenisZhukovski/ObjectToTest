using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Common.Parse;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class InstantiationStatement : IInstantiationStatement
    {
        /*
        * @todo #100 60m/DEV Implement instantiation statement parsing.
        */

        private InstantiationStatement(string codeStatement)
        {
        }

        public static ParseResult Parse(string codeStatement)
        {
            var isInstantiation = new LiteralAwareSeparateWordSubstrings(codeStatement, "new").Any();

            if (isInstantiation)
            {
                return new ParseSuccessful<IInstantiationStatement>(new InstantiationStatement(codeStatement));
            }

            return new ParseFail();
        }
    }
}