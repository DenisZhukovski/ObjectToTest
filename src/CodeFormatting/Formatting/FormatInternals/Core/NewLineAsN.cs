using System;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class NewLineAsN
    {
        private readonly string _format;

        public NewLineAsN(string format)
        {
            _format = format;
        }

        public override string ToString()
        {
            return _format.Replace(Environment.NewLine, "N");
        }
    }
}