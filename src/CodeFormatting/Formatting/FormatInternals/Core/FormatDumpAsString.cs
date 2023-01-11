using System.Text;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class FormatDumpAsString : IFormatLogger
    {
        private readonly StringBuilder _result = new();

        public void WriteLine(string dump)
        {
            _result.AppendLine(dump);
        }

        public string Result => _result.ToString();
    }
}