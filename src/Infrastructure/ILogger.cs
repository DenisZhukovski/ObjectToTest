namespace ObjectToTest.Infrastructure
{
    public interface ILogger
    {
        void WriteLine(string log);
    }

    public static class LoggerInstance
    {
        public static ILogger? Instance { get; set; }
    }
}