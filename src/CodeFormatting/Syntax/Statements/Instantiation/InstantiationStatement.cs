using System;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using ObjectToTest.CodeFormatting.Syntax.Statements.Args;
using ObjectToTest.CodeFormatting.Syntax.Statements.Assignment;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation
{
    public class InstantiationStatement : IInstantiationStatement
    {
        private readonly string _codeStatement;

        private readonly Lazy<ITypeDefinition> _type;

        private readonly Lazy<IArguments> _arguments;

        private readonly Lazy<IPropertyAssignments> _propertyAssignments;

        public InstantiationStatement(string codeStatement)
        {
            _codeStatement = codeStatement;

            _type = new(
                () =>
                    new RawTypeDefinition(
                        new SubstringBetween(codeStatement, "new ", "(").ToString()
                    )
            );

            _arguments = new(
                () =>
                {
                    var argumentsClosure = new ClosureSubstrings(
                        _codeStatement,
                        new RoundBracketsClosure(),
                        notAnalyzeIn: new LiteralsAndClosuresSubstrings(
                            _codeStatement,
                            new CurlyBracketsClosure(),
                            new SquareBracketsClosure()
                        )
                    );

                    if (argumentsClosure.Any())
                    {
                        return new Args.Arguments(argumentsClosure.First().WithoutBorders().ToString());
                    }
                    else
                    {
                        return new SkippedArguments();
                    }
                }
            );

            _propertyAssignments = new(
                () =>
                {
                    var propertiesClosure = new ClosureSubstrings(
                        _codeStatement,
                        new CurlyBracketsClosure(),
                        notAnalyzeIn: new LiteralsAndClosuresSubstrings(
                            _codeStatement,
                            new SquareBracketsClosure(),
                            new RoundBracketsClosure()
                        )
                    ).ToArray();

                    if (propertiesClosure.Any())
                    {
                        return new PropertyAssignments(propertiesClosure.First().WithoutBorders().ToString());
                    }
                    else
                    {
                        return new EmptyPropertyAssignment();
                    }
                }
            );
        }

        public ITypeDefinition Type => _type.Value;

        public IArguments Arguments => _arguments.Value;

        public IPropertyAssignments InlinePropertiesAssignment => _propertyAssignments.Value;

        public static ParseResult Parse(string codeStatement)
        {
            var isInstantiation = new LiteralAwareSeparateWordSubstrings(codeStatement, "new").Any();

            if (isInstantiation)
            {
                return new ParseSuccessful<IInstantiationStatement>(new InstantiationStatement(codeStatement));
            }

            return new ParseFail();
        }

        public override string ToString()
        {
            return _codeStatement.Trim();
        }
    }
}