using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class ArgumentsDump
    {
        private readonly IArguments _statement;
        private readonly Tabs _tabs;

        public ArgumentsDump(IArguments statementArguments, Tabs tab)
        {
            _statement = statementArguments;
            _tabs = tab;
        }

        public override string ToString()
        {
            var lines = new List<string>();
            lines.Add($"{_tabs}{_statement.GetType().Name}: {_statement.Count()}");
            lines.AddRange(_statement.Select(x => new ArgumentDump(x, _tabs.Tab()).ToString()));

            return new NewLineSeparatedString(lines.ToArray()).ToString();
        }
    }
}