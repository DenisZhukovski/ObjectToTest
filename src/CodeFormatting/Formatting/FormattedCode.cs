using ObjectToTest.CodeFormatting.Syntax.Contracts;
using System;
using ObjectToTest.CodeFormatting.Formatting.FormattedCodeInternals;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class FormattedCode
    {
        private readonly ISyntaxTree _tree;
        private readonly IFormattingRule[] _rules;
        private readonly Lazy<IFormat> _format;

        public FormattedCode(ISyntaxTree tree, params IFormattingRule[] rules)
        {
            _tree = tree;
            _rules = rules;
            _format = new(
                () =>
                {
                    var format = new SyntaxTreeFormat(tree);

                    foreach (var rule in _rules)
                    {
                        rule.ApplyTo(format);
                    }

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