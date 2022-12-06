using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Args
{
    public class RawArgument : IUnknownArgument
    {
        private readonly string _argument;

        public RawArgument(string argument)
        {
            _argument = argument;
        }

        public override string ToString()
        {
            return _argument;
        }
    }
}