using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Common.Parse;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class InstantiationStatement : IInstantiationStatement
    {
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
            Type = new RawTypeDefinition(new SubstringBetween(codeStatement, "new ", "(").ToString());

            var argumentsClosure = new LiteralAwareClosureSubstrings(codeStatement, '(', ')');
            if (argumentsClosure.Any())
            {
                Arguments = new Arguments(argumentsClosure.First().WithoutBorders().ToString());
            }
            else
            {
                Arguments = new SkippedArguments();
            }
            
            var propertiesClosure = new LiteralAwareClosureSubstrings(codeStatement, '{', '}').ToArray();
            if (propertiesClosure.Any())
            {
                InlinePropertiesAssignment = new PropertyAssignments(propertiesClosure.First().WithoutBorders().ToString());
            }
            else
            {
                InlinePropertiesAssignment = new EmptyPropertyAssignment();
            }
        }

        public ITypeDefinition Type { get; }

        public IArguments Arguments { get; }

        public IPropertyAssignments InlinePropertiesAssignment { get; }

        public static ParseResult Parse(string codeStatement)
        {
            var isInstantiation = new LiteralAwareSeparateWordSubstrings(codeStatement, "new").Any();

            if (isInstantiation)
            {
                return new ParseSuccessful<IInstantiationStatement>(new InstantiationStatement(codeStatement));
            }

            return new ParseFail();
        }
    }
}