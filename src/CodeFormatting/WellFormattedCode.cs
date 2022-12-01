namespace ObjectToTest.CodeFormatting
{
    /*
     * @todo #91 60m/DEV Create hierarchy for code formatting based on ISyntaxTree.
     */
    public class WellFormattedCode
    {
        private readonly string _notFormattedCode;

        public WellFormattedCode(string notFormattedCode)
        {
            _notFormattedCode = notFormattedCode;
        }

        public override string ToString()
        {
            return _notFormattedCode;
        }
    }
}