namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class StringWithSemicolonInTheEnd
    {
        private readonly string _source;

        public StringWithSemicolonInTheEnd(string source)
        {
            _source = source;
        }

        public override string ToString()
        {
            if (_source.EndsWith(";"))
            {
                return _source;
            }

            return _source + ";";
        }
    }
}