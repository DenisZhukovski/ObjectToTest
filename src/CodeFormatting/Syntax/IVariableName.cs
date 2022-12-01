namespace ObjectToTest.CodeFormatting.Syntax
{
    /// <summary>
    /// someCustomVariable, _privateField, "stringConstant", 1234, Values.ValueFromEnum, Singleton.Instance
    /// </summary>
    public interface IVariableName : ILeftAssignmentPart, IRightAssignmentPart, IArgument
    {
        string ToString();
    }
}