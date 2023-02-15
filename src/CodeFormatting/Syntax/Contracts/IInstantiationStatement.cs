namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    /// <summary>
    /// new Foo(), new int[] {}, dictionary or list
    /// </summary>
    public interface IInstantiationStatement : IRightAssignmentPart, ICodeStatement, IArgument
    {
        ITypeDefinition Type { get; }

        IArguments Arguments { get; }

        IInlineAssignments InlineInlinesAssignment { get; }
    }
}