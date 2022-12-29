using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class SpacesBetweenArgumentsAreRequired : IFormattingRule
    {
        public void ApplyTo(ITransformationDefinition definition)
        {
            definition.OverrideForArrayOf<IArgument>(
                x => x.ToString().Length < 80, 
                x => new Parts(
                    "(", string.Join(", ", x), ")"
                ).ToString()
            );
        }
    }
}