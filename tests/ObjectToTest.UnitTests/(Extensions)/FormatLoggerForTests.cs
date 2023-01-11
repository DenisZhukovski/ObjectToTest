using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Formatting.Core;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests.Extensions
{
    public class FormatLoggerForTests : IFormatLogger
    {
        private readonly ITestOutputHelper _output;

        public FormatLoggerForTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        public void WriteLine(string dump)
        {
            _output.WriteLine(dump);
        }
    }
}