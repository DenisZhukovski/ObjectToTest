using System.Collections.Generic;

namespace ObjectToTest.Infrastructure
{
    public class InMemoryLogger : ILogger
    {
        private readonly List<string> _records = new();

        public void WriteLine(string log)
        {
            _records.Add(log);
        }

        public List<string> Records => _records;
    }
}