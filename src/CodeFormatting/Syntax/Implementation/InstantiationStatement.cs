using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Common.Parse;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class InstantiationStatement : IInstantiationStatement
    {
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