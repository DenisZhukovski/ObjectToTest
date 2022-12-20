using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class ArgumentsBiggerThan80SpacesShouldBeEachFromSeparateLine : IFormattingRule
    {
        public void ApplyTo(ITransformationDefinition definition)
        {
            definition.OverrideForArrayOf<IArgument>(
                x => x.ToString().Length > 80,
                (arguments, tabs) => new FormatWithSameLineOpenBracketAndEachArgFromNewLine(arguments, tabs).Format()
            );
        }
    }
}