using ObjectToTest.Infrastructure;

namespace ObjectToTest.CodeFormatting.Formatting.FormatInternals.Core
{
    public class LoggerWithIndentation : ILogger
    {
        private readonly ILogger _logger;
        private Tabs _indentation;

        public LoggerWithIndentation(ILogger logger, Tabs tabs)
        {
            _logger = logger;
            _indentation = tabs;
        }

        public void Tab()
        {
            _indentation = _indentation.Tab();
        }

        public void WriteLine(string log)
        {
            _logger.WriteLine($"{_indentation}{log}");
        }
    }
}