using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax;
using ObjectToTest.CodeFormatting.Syntax.Extensions;
using ObjectToTest.Infrastructure;

namespace ObjectToTest.CodeFormatting
{
    public class WellFormattedCode
    {
        private readonly string _notFormattedCode;
        private readonly ILogger _logger;

        public WellFormattedCode(string notFormattedCode) : this(notFormattedCode, new SilentLogger())
        {
        }

        public WellFormattedCode(string notFormattedCode, ILogger logger)
        {
            _notFormattedCode = notFormattedCode;
            _logger = logger;
        }

        public override string ToString()
        {
            var syntaxTree = new SyntaxTree(_notFormattedCode);
            _logger.WriteLine("Dump of SyntaxTree");
            _logger.WriteLine(syntaxTree.Dump());
            _logger.WriteLine("Dump of SyntaxTree ended.");

            return new FormattedCode(
                _logger,
                syntaxTree,
                new EachNewStatementShouldBeFromSeparateLine(),
                new SpacesBetweenArgumentsAreRequired(),
                new ArgumentsBiggerThan80SpacesShouldBeEachFromSeparateLine(),
                new ArgumentsThatContainsInstantiationShouldBeEachFromSeparateLine(),
                new ArgumentsThatContainsLambdasShouldBeEachFromSeparateLine(),
                new InlinePropertiesAssignmentsShouldBeEachFromSeparateLine()
            ).ToString();
        }
    }
}