namespace ObjectToTest.Infrastructure
{
    public class SilentLogger : ILogger
    {
        public void WriteLine(string log)
        {
            // Nothing to do because we should keep silence
        }
    }
}