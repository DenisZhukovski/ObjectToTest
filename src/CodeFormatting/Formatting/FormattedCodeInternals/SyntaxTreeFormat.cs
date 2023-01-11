using System;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Formatting.FormattedCodeInternals
{
    public class SyntaxTreeFormat : IFormat
    {
        private readonly Format _format = new();

        public SyntaxTreeFormat(params IFormattingRule[] rules)
            : this((IEnumerable<IFormattingRule>)rules)
        {
        }
        
        public SyntaxTreeFormat(IEnumerable<IFormattingRule> rules)
        {
            _format.ForArrayOf<ICodeStatement>(x => string.Join(";", x));
            _format.For<IInstantiationStatement>(
                "new {0}{1}{2}", 
                x => new Args(x.Type, x.Arguments, x.InlinePropertiesAssignment)
            );
            _format.For<IUnknownCodeStatement>("{x}", x => new Args(x.ToString()));
            _format.ForArrayOf<IArgument>(
                x => new Parts(
                    "(", string.Join(",", x), ")"
                ).ToString()
            );
            _format.For<IArgument>("{0}", x => new Args(x.ToString().Trim()));
            _format.For<ITypeDefinition>("{0}", x => new Args(x.ToString().Trim()));
            foreach (var rule in rules)
            {
                rule.ApplyTo(_format);
            }
        }

        public string ApplyTo(object item)
        {
            return _format.ApplyTo(item);
        }

        public void Add(INodeFormat format)
        { 
            _format.Add(format);
        }

        public void AddAsFirst(INodeFormat format)
        {
            _format.AddAsFirst(format);
        }

        public void AddAsFirst(INodeTransformation transformation)
        {
            _format.AddAsFirst(transformation);
        }

        public void Add(INodeTransformation transformation)
        {
            _format.Add(transformation);
        }
    }
}