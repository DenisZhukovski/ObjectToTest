using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Statements.Assignment;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class InlinePropertiesAssignmentsShouldBeEachFromSeparateLine : IFormattingRule
    {
        public void ApplyTo(IFormat definition)
        {
            definition.OverrideForArrayOf<IAssignmentPart>(
                x => x is PropertyAssignments,
                (arguments, tabs) => new FormatWithNewLineOpenCurlyBracketAndEachArgFromNewLine(arguments, tabs).Format(),
                nameof(InlinePropertiesAssignmentsShouldBeEachFromSeparateLine)
            );

            definition.For<IAssignmentPart>("{0} = {1}", x => new Args(x.Left, x.Right), "Assignment base format");
        }
    }
}