using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Unknown
{
    public class UnknownCodeStatement : IUnknownCodeStatement
    {
        private readonly string _codeStatement;

        public UnknownCodeStatement(string codeStatement)
        {
            _codeStatement = codeStatement;
        }

        public override string ToString()
        {
            return _codeStatement;
        }
    }
}