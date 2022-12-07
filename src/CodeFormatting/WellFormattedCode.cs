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
                new SpacesBetweenArgumentsAreRequired()
            ).ToString();

            /*
            * @todo #98 60m/DEV Rework infrastructure for code formatting.
             *
             * Ideally it should look like:
             *
             * new FormattedSyntaxTree(new SyntaxTree(_notFormattedCode),
             *    new MakeOneSpaceBetweenArguments(),
             *    new IndentionShouldBe(4.Spaces()),
             *    new LineBiggerThanXSpacesShouldBeFormatted(80.Spaces()),
             *    new LambdaShouldBeFormatted(),
             *    new PropertiesShouldBeInitializedFromSeparateLines()
             * );
             *
             * Probably more rules.
            */
        }
    }
}