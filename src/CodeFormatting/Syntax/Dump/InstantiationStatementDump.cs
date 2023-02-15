using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class InstantiationStatementDump
    {
        private readonly IInstantiationStatement _statement;
        private readonly Tabs _tabs;

        public InstantiationStatementDump(IInstantiationStatement statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            return new NewLineSeparatedString(
                $"{_tabs}{_statement.GetType().Name}",
                $"{_tabs.Tab()}Type: {_statement.Type.ToString()}",
                $"{new ArgumentsDump(_statement.Arguments, _tabs.Tab())}",
                $"{new PropertiesDump(_statement.InlinePropertiesAssignment, _tabs.Tab())}"
            ).ToString();
        }
    }
}