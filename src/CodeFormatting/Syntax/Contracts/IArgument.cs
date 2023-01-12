namespace ObjectToTest.CodeFormatting.Syntax.Contracts
{
    /// <summary>
    /// new Foo([argument]).
    /// foo.Run([argument]).
    /// </summary>
    public interface IArgument
    {
    }

    public interface ILiteral : IArgument
    {

    }
}