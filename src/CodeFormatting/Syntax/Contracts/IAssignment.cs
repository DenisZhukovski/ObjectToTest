namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    /// <summary>
    /// [Left] = [Right];
    /// var i = 5;
    /// int a = 3;
    /// a = "234"
    /// a = () => { Do(); }
    /// var some = new Foo(bar) {A = 5};
    /// </summary>
    public interface IAssignment : ICodeStatement
    {
        ILeftAssignmentPart Left { get; }

        IRightAssignmentPart Right { get; }
    }
}