namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    /// <summary>
    /// new Foo(), new int[] {}, dictionary or list
    /// </summary>
    public interface IInstantiationStatement : IRightAssignmentPart, ICodeStatement, IArgument
    {
        /*
        * @todo #125 60m/LEAD IInstantiationStatement is ICodeStatement and IArgument at the same time.
         *
         * It may not be an issue, but need to analyze if this can affect
         * formatting. Current implementation relies on conditions like (x is IArgument)
         * so top level IInstantiationStatement could be treated as IArgument and formatted as IArgument.
         *
         * Most probably this is not a case.
        */

        ITypeDefinition Type { get; }

        IArguments Arguments { get; }

        IPropertyAssignments InlinePropertiesAssignment { get; }
    }
}