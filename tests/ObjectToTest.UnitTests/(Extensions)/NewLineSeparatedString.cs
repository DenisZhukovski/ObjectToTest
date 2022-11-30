using System;

namespace ObjectToTest.UnitTests.Extensions
{
    public class NewLineSeparatedString
    {
        private readonly string[] _lines;

        public NewLineSeparatedString(params string[] lines)
        {
            _lines = lines;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _lines);
        }
    }
}