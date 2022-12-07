namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface IFormattingRule
    {
        void ApplyTo(ITransformationDefinition definition);
    }
}