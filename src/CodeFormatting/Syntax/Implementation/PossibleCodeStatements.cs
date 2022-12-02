using System;
using ObjectToTest.CodeFormatting.Syntax.Common.Parse;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class PossibleCodeStatements : IPossibleItems<ICodeStatement>
    {
        /*
         * @todo #97 60m/DEV Implement different statements and register them here.
         */
        private readonly Lazy<PossibleItems<ICodeStatement>> _possibleStatements =
            new(() =>
                new PossibleItems<ICodeStatement>(
                    InstantiationStatement.Parse,
                    codeStatement => new ParseSuccessful<ICodeStatement>(new UnknownCodeStatement(codeStatement))
                )
            );

        public ICodeStatement BestMatch(string value)
        {
            return _possibleStatements.Value.BestMatch(value);
        }
    }
}