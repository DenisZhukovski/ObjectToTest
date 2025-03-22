using ObjectToTest.Infrastructure;
using Xunit;

namespace ObjectToTest.UnitTests.Extensions
{
    public class LoggerForTests(ITestOutputHelper output) : ILogger
    {
        public void WriteLine(string log)
        {
            output.WriteLine(log);
        }
    }
}