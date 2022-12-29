using ObjectToTest.CodeFormatting.Syntax.Contracts;
using System;
using ObjectToTest.CodeFormatting.Formatting.FormattedCodeInternals;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class FormattedCode
    {
        private readonly ISyntaxTree _tree;
        private readonly Lazy<IFormat> _format;

        public FormattedCode(ISyntaxTree tree, params IFormattingRule[] rules)
        {
            _tree = tree;
            _format = new(() => new SyntaxTreeFormat(rules));
        }

        public override string ToString()
        {
            return _format.Value.ApplyTo(_tree);
        }
    }
}