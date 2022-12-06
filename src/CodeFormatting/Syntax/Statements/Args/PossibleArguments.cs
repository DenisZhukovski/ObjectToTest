using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Parse;
using ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Args
{
    public class PossibleArguments : IPossibleItems<IArgument>
    {
        /*
        * @todo #106 60m/DEV Add lambdas support.
        */
        private readonly PossibleItems<IArgument> _possibleStatements;

        public PossibleArguments()
        {
            _possibleStatements = new PossibleItems<IArgument>(
                InstantiationStatement.Parse,
                Literal.Parse,
                codeStatement => new ParseSuccessful<IArgument>(new RawArgument(codeStatement))
            );
        }

        public IArgument BestMatch(string value)
        {
            return _possibleStatements.BestMatch(value);
        }
    }
}