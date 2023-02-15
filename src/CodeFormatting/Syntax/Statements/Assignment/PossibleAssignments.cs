using System;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class PossibleAssignments : IPossibleItems<IAssignmentPart>
    {
        /*
        * @todo #76 60m/DEV Add assignment with type definition.
        */

        /*
        * @todo #76 60m/DEV Add assignment to variable.
        */

        private readonly Lazy<PossibleItems<IAssignmentPart>> _possibleStatements = new(
            () =>
                new PossibleItems<IAssignmentPart>(
                    PropertyAssignment.Parse,
                    codeStatement => new ParseSuccessful<IAssignmentPart>(new RawAssignment(codeStatement))
                )
        );

        public IAssignmentPart BestMatch(string value)
        {
            return _possibleStatements.Value.BestMatch(value);
        }
    }
}