using System;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class DictionaryInlineAssignmentDump
    {
        private readonly IDictionaryInlineAssignment _statement;
        private readonly Tabs _tabs;

        public DictionaryInlineAssignmentDump(IDictionaryInlineAssignment statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            return new NewLineSeparatedString(
                $"{_tabs}{_statement.GetType().Name}",
                $"{_tabs.Tab()}Key:{Environment.NewLine}{new CodeStatementDump(_statement.Key, _tabs.Tab().Tab())}",
                $"{_tabs.Tab()}Value:{Environment.NewLine}{new CodeStatementDump(_statement.Value, _tabs.Tab().Tab())}"
            ).ToString();
        }
    }
}