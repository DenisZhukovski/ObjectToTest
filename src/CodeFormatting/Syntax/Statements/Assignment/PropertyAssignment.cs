using System;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class PropertyAssignment : IAssignmentPart
    {
        private readonly string _code;
        private readonly Lazy<PossibleCodeStatements> _possibleStatements = new(() => new());

        private PropertyAssignment(string code)
        {
            _code = code;
            var parts = code.Split('=');
            Left = new RawAssignmentPart(parts.First().Trim());

            var statement = _possibleStatements.Value.BestMatch(parts.Last());

            if (statement is IRightAssignmentPart rightAssignmentPart)
            {
                Right = rightAssignmentPart;
            }
            else
            {
                Right = new RawAssignmentPart(parts.Last().Trim().ToString());
            }
        }

        public ILeftAssignmentPart Left { get; }

        public IRightAssignmentPart Right { get; }

        public static ParseResult Parse(string codeStatement)
        {
            var isInstantiation = new LiteralAndClosureAwareSeparateWordSubstrings(codeStatement, "=").Any();

            if (isInstantiation)
            {
                return new ParseSuccessful<IAssignmentPart>(new PropertyAssignment(codeStatement));
            }

            return new ParseFail();
        }

        public override string ToString()
        {
            return _code.Trim();
        }
    }
}