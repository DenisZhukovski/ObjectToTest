using System;
using System.Security.Cryptography.X509Certificates;
using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Formatting.FormattedCodeInternals
{
    public class SyntaxTreeFormat : IFormat, ITransformationDefinition
    {
        private readonly ISyntaxTree _syntaxTree;
        private readonly Format _format = new Format();

        public SyntaxTreeFormat(ISyntaxTree syntaxTree)
        {
            _syntaxTree = syntaxTree;

            _format.ForArrayOf<ICodeStatement>(x =>
                {
                    return String.Join(";", x);
                }
            );
            _format.For<IInstantiationStatement>("new {0}({1}){2}", x => new Args(x.Type, x.Arguments, x.InlinePropertiesAssignment));
            _format.For<IUnknownCodeStatement>("{x}", x => new Args(x.ToString()));
            _format.ForArrayOf<IArgument>(x => String.Join(",", x));
            _format.For<IArgument>("{0}", x => new Args(x.ToString().Trim()));
            _format.For<ITypeDefinition>("{0}", x => new Args(x.ToString().Trim()));
        }

        public string ApplyTo(object item)
        {
            return _format.ApplyTo(item);
        }

        public void OverrideForArrayOf<T>(Func<string[], string> format)
        {
            _format.OverrideForArrayOf<T>(format);
        }

        public void If(Func<object, bool> condition, IObjectWithFormat format)
        {
            _format.If(condition, format);
        }

        public void If(Func<object, bool> isApplicable, Func<string, string> transform)
        {
            _format.If(isApplicable, transform);
        }
    }
}