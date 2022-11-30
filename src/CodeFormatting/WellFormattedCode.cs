namespace ObjectToTest.CodeFormatting
{
    /*
     * @todo #85 60m/DEV Decompose code formatting to create syntax tree from code.
     * Better to parse code and format it properly. It isolates existing functionality.
     * Probably need to invest some time to find existing ability to create syntax tree from code.
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