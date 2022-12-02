using ObjectToTest.CodeFormatting.Syntax.Common;
using ObjectToTest.CodeFormatting.Syntax.Common.Parse;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class PossibleCodeStatements : IPossibleItems<ICodeStatement>
    {
        /*
         * @todo #97 60m/DEV Implement different statements and register them here.
         */
        private readonly PossibleItems<ICodeStatement> _possibleStatements;

        public PossibleCodeStatements()
        {
            _possibleStatements = new PossibleItems<ICodeStatement>(
                codeStatement => InstantiationStatement.Parse(codeStatement),
                codeStatement => new ParseSuccessful<ICodeStatement>(new UnknownCodeStatement(codeStatement))
            );
        }

        public ICodeStatement BestMatch(string value)
        {
            return _possibleStatements.BestMatch(value);
        }
    }
}