using System;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class Assignment : IAssignment
    {
        private readonly string _code;
        private readonly Lazy<PossibleCodeStatements> _possibleStatements = new(() => new());

        private Assignment(string code)
        {
            _code = code;
            var parts = new CharacterSeparatedSubstrings(code, '=', notAnalyzeIn: new LiteralsAndClosuresSubstrings(code));
            Left = new RawAssignmentPart(parts.First().ToString().Trim());

            var statement = _possibleStatements.Value.BestMatch(parts.Last().ToString());

            if (statement is IRightAssignmentPart rightAssignmentPart)
            {
                Right = rightAssignmentPart;
            }
            else
            {
                Right = new PossibleRightAssignmentParts().BestMatch(parts.Last().ToString().Trim().ToString());
            }
        }

        public ILeftAssignmentPart Left { get; }

        public IRightAssignmentPart Right { get; }

        public static ParseResult Parse(string codeStatement)
        {
            var isInstantiation = new LiteralAndClosureAwareSeparateWordSubstrings(codeStatement, "=").Any();

            if (isInstantiation)
            {
                return new ParseSuccessful<IAssignment>(new Assignment(codeStatement));
            }

            return new ParseFail();
        }

        public override string ToString()
        {
            return _code.Trim();
        }
    }
}