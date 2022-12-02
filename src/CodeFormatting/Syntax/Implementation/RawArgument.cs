namespace ObjectToTest.CodeFormatting.Syntax.Implementation
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