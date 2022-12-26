using System;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class SyntaxTreeDump
    {
        private readonly ISyntaxTree _tree;
        private readonly Lazy<Format> _format;

        public SyntaxTreeDump(ISyntaxTree tree)
        {
            _tree = tree;
            _format = new(
                () =>
                {
                    var format = new Format();
                    format.ForArrayOf<ICodeStatement>(x => string.Join("", x));
                    format.For<IInstantiationStatement>(new NewLineSeparatedString("- {0}: {1}", "{2}", "{3}").ToString(), x => new Args(x.GetType().Name, x.Type, x.Arguments, x.InlinePropertiesAssignment));
                    format.For<IUnknownCodeStatement>("{x}", x => new Args(x.ToString()));
                    format.OverrideForArrayOf<IArgument>(x => x is IEnumerable<IArgument>, (x, tabs) => new FormatWithHeaderAndEachFromNewLine(args => $"- Arguments: {args.Length}", x, tabs).Format());
                    format.For<IArgument>("{0}({1})", x => new Args(x.GetType().Name, x.ToString().Trim()));
                    format.For<ITypeDefinition>("{0}", x => new Args(x.ToString().Trim()));
                    return format;
                }
            );
        }

        public override string ToString()
        {
            return _format.Value.ApplyTo(_tree);
        }
    }
}