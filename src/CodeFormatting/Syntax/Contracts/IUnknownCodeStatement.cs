namespace ObjectToTest.CodeFormatting.Syntax
{
    public interface IUnknownCodeStatement : ICodeStatement
    {
        string ToString();
    }

    public class UnknownCodeStatement : IUnknownCodeStatement
    {
        private readonly string _codeStatement;

        public UnknownCodeStatement(string codeStatement)
        {
            _codeStatement = codeStatement;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}