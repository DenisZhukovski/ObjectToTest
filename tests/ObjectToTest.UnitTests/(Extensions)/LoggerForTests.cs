using ObjectToTest.Infrastructure;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests.Extensions
{
    public class LoggerForTests : ILogger
    {
        private readonly ITestOutputHelper _output;

        public LoggerForTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        public void WriteLine(string log)
        {
            _output.WriteLine(log);
        }
    }
}