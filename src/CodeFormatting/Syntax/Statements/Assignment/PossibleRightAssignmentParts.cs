using System;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Statements.Args;
using ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class PossibleRightAssignmentParts : IPossibleItems<IRightAssignmentPart>
    {
        private readonly Lazy<PossibleItems<IRightAssignmentPart>> _possibleStatements = new(
            () =>
                new PossibleItems<IRightAssignmentPart>(
                    LambdaExpression.Parse,
                    InstantiationStatement.Parse,
                    Literal.Parse,
                    codeStatement => new ParseSuccessful<IRightAssignmentPart>(new RawAssignmentPart(codeStatement))
                )
        );

        public IRightAssignmentPart BestMatch(string value)
        {
            return _possibleStatements.Value.BestMatch(value);
        }
    }
}