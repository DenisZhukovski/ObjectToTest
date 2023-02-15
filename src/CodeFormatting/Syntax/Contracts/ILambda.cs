namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    /// <summary>
    /// () => { },
    /// () => { /*multiline code or comments*/,
    /// x => x + 2,
    /// _ => 42,
    /// (x, y) => { return x + y; }
    /// </summary>
    public interface ILambda : IRightAssignmentPart, ICodeStatement, IArgument
    {
    }
}