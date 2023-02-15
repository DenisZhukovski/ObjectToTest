namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    public interface IDictionaryInlineAssignment : ICodeStatement
    {
        ICodeStatement Key { get; }

        ICodeStatement Value { get; }
    }
}