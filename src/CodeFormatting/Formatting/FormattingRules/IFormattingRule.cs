namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface IFormattingRule
    {
        void ApplyTo(IFormat definition);
    }
}