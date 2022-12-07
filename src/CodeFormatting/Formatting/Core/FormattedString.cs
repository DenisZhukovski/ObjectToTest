namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class FormattedString
    {
        private readonly string _format;
        private readonly string[] _args;

        public FormattedString(string format, params string[] args)
        {
            _format = format;
            _args = args;
        }

        public override string ToString()
        {
            return string.Format(_format, _args);
        }
    }
}