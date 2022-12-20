using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Statements.Assignment;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class InlinePropertiesAssignmentsShouldBeEachFromSeparateLine : IFormattingRule
    {
        public void ApplyTo(ITransformationDefinition definition)
        {
            definition.OverrideForArrayOf<IAssignmentPart>(
                x => x is PropertyAssignments,
                (arguments, tabs) => new FormatWithNewLineOpenCurlyBracketAndEachArgFromNewLine(arguments, tabs).Format()
            );
        }
    }
}