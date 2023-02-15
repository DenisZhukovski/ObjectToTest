using System;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class DictionaryInlineAssignment : IDictionaryInlineAssignment
    {
        private readonly Lazy<PossibleCodeStatements> _codeStatements = new(() => new PossibleCodeStatements());
        private readonly Lazy<ICodeStatement> _key;
        private readonly Lazy<ICodeStatement> _value;

        private DictionaryInlineAssignment(string codeStatement)
        {
            var leftAndRight = new Lazy<CharacterSeparatedSubstrings>(
                () =>
                {
                    var closure = new ClosureSubstrings(
                        codeStatement,
                        new CurlyBracketsClosure(),
                        notAnalyzeIn: new LiteralsAndClosuresSubstrings(
                            codeStatement,
                            new RoundBracketsClosure(),
                            new SquareBracketsClosure(),
                            new AngleBracketsClosure()
                        )
                    ).First().WithoutBorders();

                    var leftAndRight = new CharacterSeparatedSubstrings(
                        closure.ToString(),
                        ',',
                        notAnalyzeIn: new LiteralsAndClosuresSubstrings(closure.ToString())
                    );

                    return leftAndRight;
                }
            );

            _key = new Lazy<ICodeStatement>(
                () => _codeStatements.Value.BestMatch(leftAndRight.Value.First().ToString())
            );

            _value = new Lazy<ICodeStatement>(
                () => _codeStatements.Value.BestMatch(leftAndRight.Value.Last().ToString())
            );
        }

        public ICodeStatement Key => _key.Value;

        public ICodeStatement Value => _value.Value;

        public static ParseResult Parse(string codeStatement)
        {
            var closure = new ClosureSubstrings(
                codeStatement,
                new CurlyBracketsClosure(),
                notAnalyzeIn: new LiteralsAndClosuresSubstrings(
                    codeStatement,
                    new RoundBracketsClosure(),
                    new SquareBracketsClosure(),
                    new AngleBracketsClosure()
                )
            );

            if (closure.Count() == 1)
            {
                if (new TrailingPartsContainOnlyWhiteSpaces(codeStatement, closure.First()).IsTrue)
                {
                    return new ParseSuccessful(new DictionaryInlineAssignment(codeStatement));
                }
            }

            return new ParseFail();
        }
    }
}