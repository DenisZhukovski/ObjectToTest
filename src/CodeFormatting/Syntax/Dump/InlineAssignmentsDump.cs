using System;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class InlineAssignmentsDump
    {
        private readonly IInlineAssignments _statement;
        private readonly Tabs _tabs;

        public InlineAssignmentsDump(IInlineAssignments statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            var lines = new List<string>();
            lines.Add($"{_tabs}{_statement.GetType().Name}: {_statement.Count()}");
            lines.AddRange(_statement.Select(x =>
                {
                    return x switch
                    {
                        IAssignment assignmentPart => new AssignmentDump(assignmentPart, _tabs.Tab()).ToString(), 
                        IDictionaryInlineAssignment dictionaryInlineAssignment => $"{_tabs}Dictionary Item:{Environment.NewLine}{new DictionaryInlineAssignmentDump(dictionaryInlineAssignment, _tabs.Tab())}",
                        IInstantiationStatement instantiationStatement => new InstantiationStatementDump(instantiationStatement, _tabs.Tab()).ToString(),
                        ILiteral literal => $"{_tabs.Tab()}Literal: {literal}",
                        ILambda lambda => $"{_tabs.Tab()}Lambda: {lambda}",
                        IUnknownCodeStatement unknownCodeStatement => $"{_tabs.Tab()}Cannot parse (look into ArgumentDump for more details): {unknownCodeStatement}",
                        _ => $"{_tabs.Tab()}Cannot dump (look into RightAssignmentPartDump for more details): {x.GetType().Name}-{x}"
                    };
                }
            ));

            return new NewLineSeparatedString(lines.ToArray()).ToString();
        }
    }
}