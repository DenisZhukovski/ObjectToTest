using System;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class AssignmentDump
    {
        private readonly IAssignment _statement;
        private readonly Tabs _tabs;

        public AssignmentDump(IAssignment statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            var lines = new List<string>();
            lines.Add($"{_tabs}{_statement.GetType().Name}");
            lines.Add($"{_tabs.Tab()}Left Part: {_statement.Left}");
            lines.Add($"{_tabs.Tab()}Value:{Environment.NewLine}{new RightAssignmentPartDump(_statement.Right, _tabs.Tab().Tab())}");

            return new NewLineSeparatedString(lines.ToArray()).ToString();
        }
    }
}