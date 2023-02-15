using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation
{
    public class LambdaExpression : ILambda
    {
        private readonly string _codeStatement;

        public LambdaExpression(string codeStatement)
        {
            _codeStatement = codeStatement;
        }

        public static ParseResult Parse(string codeStatement)
        {
            var isInstantiation = new LiteralAndClosureAwareSeparateWordSubstrings(codeStatement, "=>").Any();

            if (isInstantiation)
            {
                return new ParseSuccessful<ILambda>(new LambdaExpression(codeStatement));
            }

            return new ParseFail();
        }

        public override string ToString()
        {
            return _codeStatement.Trim();
        }
    }
}