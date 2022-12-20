using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax;

namespace ObjectToTest.CodeFormatting
{
    public class WellFormattedCode
    {
        private readonly string _notFormattedCode;

        public WellFormattedCode(string notFormattedCode)
        {
            _notFormattedCode = notFormattedCode;
        }

        public override string ToString()
        {
            return new FormattedCode(
                new SyntaxTree(_notFormattedCode),
                new SpacesBetweenArgumentsAreRequired(),
                new ArgumentsBiggerThan80SpacesShouldBeEachFromSeparateLine(),
                new ArgumentsThatContainsInstantiationShouldBeEachFromSeparateLine(),
                new InlinePropertiesAssignmentsShouldBeEachFromSeparateLine()
            ).ToString();
        }
    }
}