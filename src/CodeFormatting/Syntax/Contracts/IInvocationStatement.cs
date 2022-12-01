namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    /// <summary>
    /// foo.Run(arg1, arg2);
    /// </summary>
    public interface IInvocationStatement : IRightAssignmentPart, IArgument
    {
    }
}