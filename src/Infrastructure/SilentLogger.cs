namespace ObjectToTest.Infrastructure
{
    public class SilentLogger : ILogger
    {
        public void WriteLine(string log)
        {
        }
    }
}