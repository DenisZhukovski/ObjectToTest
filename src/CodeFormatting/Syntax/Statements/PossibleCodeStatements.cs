using System;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Statements.Args;
using ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation;
using ObjectToTest.CodeFormatting.Syntax.Statements.Unknown;

namespace ObjectToTest.CodeFormatting.Syntax.Statements
{
    public class PossibleCodeStatements : IPossibleItems<ICodeStatement>
    {
        /*
         * @todo #125 60m/DEV Implement invocation statement.
         */

        /*
        * @todo #125 60m/DEV Implement assignment statement.
        */
        private readonly Lazy<PossibleItems<ICodeStatement>> _possibleStatements = new(
            () =>
                new PossibleItems<ICodeStatement>(
                    InstantiationStatement.Parse,
                    Literal.Parse,
                    codeStatement => new ParseSuccessful<ICodeStatement>(new UnknownCodeStatement(codeStatement))
                )
        );

        public ICodeStatement BestMatch(string value)
        {
            return _possibleStatements.Value.BestMatch(value);
        }
    }
}