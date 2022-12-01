namespace ObjectToTest.CodeFormatting.Syntax
{
    /// <summary>
    /// var a
    /// int i
    /// Foo bar
    /// [Type] [VariableName]
    /// </summary>
    public interface IVariableDeclaration : ILeftAssignmentPart
    {
        ITypeDefinition Type { get; }

        IVariableName VariableName { get; }
    }
}