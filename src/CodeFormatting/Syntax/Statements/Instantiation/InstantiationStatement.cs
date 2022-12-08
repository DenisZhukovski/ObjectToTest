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

        /*
        * @todo #103 60m/DEV Think about array assignment.
         * It is not required right now, but to reuse this class when instantiation statement is used
         * as a constructor argument or as an inline property assignment, it is necessary to support it.
         *
         * Like: new [] { some, thing }.
         * Most likely it is necessary to search for new .. ( and after that in addition for new ].
        */

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
                    var argumentsClosure = new LiteralAwareClosureSubstrings(_codeStatement, '(', ')');
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
                    var propertiesClosure = new LiteralAwareClosureSubstrings(_codeStatement, '{', '}').ToArray();
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