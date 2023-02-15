using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class PropertiesDump
    {
        private readonly IPropertyAssignments _statement;
        private readonly Tabs _tabs;

        public PropertiesDump(IPropertyAssignments statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            var lines = new List<string>();
            lines.Add($"{_tabs}{_statement.GetType().Name}: {_statement.Count()}");
            lines.AddRange(_statement.Select(x => new PropertyDump(x, _tabs.Tab()).ToString()));

            return new NewLineSeparatedString(lines.ToArray()).ToString();
        }
    }
}