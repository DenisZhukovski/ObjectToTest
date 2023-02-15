using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using ObjectToTest.CodeFormatting.Syntax.Statements.Args;
using ObjectToTest.CodeFormatting.Syntax.Statements.Unknown;

namespace ObjectToTest.CodeFormatting.Syntax.Dump
{
    public class SyntaxTreeDump
    {
        private readonly ISyntaxTree _tree;
        private readonly Tabs _tabs;

        public SyntaxTreeDump(ISyntaxTree tree, Tabs tabs)
        {
            _tree = tree;
            _tabs = tabs;
        }

        public override string ToString()
        {
            var items = new List<string>();

            foreach (var node in _tree)
            {
                if (node is IUnknownCodeStatement)
                {
                    items.Add($"{_tabs}Cannot parse (look into CodeStatementDump for more details): {node}");
                }
                else
                {
                    items.Add(new CodeStatementDump(node, _tabs).ToString());
                }
            }

            return new NewLineSeparatedString(items.ToArray()).ToString();
        }
    }
}