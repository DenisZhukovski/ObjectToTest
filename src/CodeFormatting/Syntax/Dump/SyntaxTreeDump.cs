using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using ObjectToTest.CodeFormatting.Syntax.Statements.Args;
using ObjectToTest.CodeFormatting.Syntax.Statements.Assignment;
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

    public class CodeStatementDump : ICodeStatement
    {
        private readonly ICodeStatement _statement;
        private readonly Tabs _tabs;

        public CodeStatementDump(ICodeStatement statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            switch (_statement)
            {
                case IInstantiationStatement instantiationStatement:
                    return new InstantiationStatementDump(instantiationStatement, _tabs).ToString();
                case IUnknownCodeStatement unknownCodeStatement:
                    return $"{_tabs}Cannot parse (look into CodeStatementDump for more details): {unknownCodeStatement}";
                default:
                    return $"{_tabs}Cannot dump (look into CodeStatementDump for more details): {_statement.GetType().Name}-{_statement}";
            }
        }
    }

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
            lines.Add($"{_tabs}Arguments: {_statement.GetType().Name}");
            lines.AddRange(_statement.Select(x => new ArgumentDump(x, _tabs.Tab()).ToString()));

            return new NewLineSeparatedString(lines.ToArray()).ToString();
        }
    }

    public class ArgumentDump
    {
        private readonly IArgument _statement;
        private readonly Tabs _tabs;

        public ArgumentDump(IArgument statement, Tabs tab)
        {
            _statement = statement;
            _tabs = tab;
        }

        public override string ToString()
        {
            switch (_statement)
            {
                case IInstantiationStatement instantiationStatement:
                    return new InstantiationStatementDump(instantiationStatement, _tabs).ToString();
                case IUnknownArgument unknownArgument:
                    return $"{_tabs}Cannot parse (look into ArgumentDump for more details): {unknownArgument}";
                case ILiteral literal:
                    return $"{_tabs}Literal: {literal}";
                default:
                    return $"{_tabs}Cannot dump (look into ArgumentDump for more details): {_statement.GetType().Name}-{_statement}";
            }
        }
    }

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
            lines.Add($"{_tabs}{_statement.GetType().Name}");
            lines.AddRange(_statement.Select(x => new PropertyDump(x, _tabs.Tab()).ToString()));

            return new NewLineSeparatedString(lines.ToArray()).ToString();
        }
    }

    public class PropertyDump
    {
        private readonly IAssignmentPart _statement;
        private readonly Tabs _tabs;

        public PropertyDump(IAssignmentPart statement, Tabs tabs)
        {
            _statement = statement;
            _tabs = tabs;
        }

        public override string ToString()
        {
            switch (_statement)
            {
                case RawAssignment rawAssignment:
                    return $"{_tabs}Cannot parse (look into PropertyDump for more details): {rawAssignment}";
                default:
                    return $"{_tabs}Cannot dump (look into PropertyDump for more details): {_statement.GetType().Name}-{_statement}.";
            }
        }
    }


}