using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using ObjectToTest.CodeFormatting.Formatting.FormattedCodeInternals;
using ObjectToTest.Extensions;

namespace ObjectToTest.CodeFormatting.Formatting
{
    
    public class FormattedCode
    {
        /*
        * @todo #98 60m/DEV Implement preconfigured pipeline for code formatting.
        */

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

    public interface IFormattingRule
    {
        void ApplyTo(ITransformationDefinition definition);
    }

    public class SpacesBetweenArgumentsAreRequired : IFormattingRule
    {
        public void ApplyTo(ITransformationDefinition definition)
        {
            definition.OverrideForArrayOf<IArgument>(x => string.Join(", ", x));
        }
    }

    public class Transformation<T> : ITransformation
    {
        private readonly Func<T, T> _transform;

        public Transformation(Func<T, T> transform)
        {
            _transform = transform;
        }

        public bool IsApplicableFor(object item)
        {
            return item is T;
        }

        public T1 ApplyTo<T1>(T1 origin)
        {
            return _transform(origin.CastTo<T>()).CastTo<T1>();
        }
    }

    public static class ObjectExtensions
    {
        public static T CastTo<T>(this object self)
        {
            return (T)self;
        }
    }

    public interface ITransformation
    {
        bool IsApplicableFor(object item);

        T ApplyTo<T>(T origin);
    }

    public static class TransformationExtensions
    {
        public static T TryToApplyTo<T>(this ITransformation self, T origin, Func<T, T> otherwise)
        {
            if (self.IsApplicableFor(origin))
            {
                return self.ApplyTo(origin);
            }

            return otherwise(origin);
        }

        public static T TryToApplyTo<T>(this ITransformation self, T origin)
        {
            if (self.IsApplicableFor(origin))
            {
                return self.ApplyTo(origin);
            }

            return origin;
        }
    }

    public class ClearArguments : IArguments
    {
        private readonly IArguments _arguments;

        public ClearArguments(IArguments arguments)
        {
            _arguments = arguments;
        }

        public IEnumerator<IArgument> GetEnumerator()
        {
            return _arguments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(",", this.Select(x => x.ToString()));
        }
    }


    public class TransformedSyntaxTree : ISyntaxTree, IFormatted<ISyntaxTree>
    {
        /*
        * @todo #98 60m/DEV Write tests. It is vital to cover this functionality to ensure all parts of code are processed by transformation.
        */

        private readonly ISyntaxTree _origin;
        private readonly ITransformation _transformation;

        public TransformedSyntaxTree(ISyntaxTree origin, ITransformation transformation)
        {
            _origin = origin;
            _transformation = transformation;
        }

        public IEnumerator<ICodeStatement> GetEnumerator()
        {
            foreach (var codeStatement in _origin)
            {
                if (_transformation.IsApplicableFor(codeStatement))
                {
                    yield return _transformation.ApplyTo(codeStatement);
                }
                else
                {
                    switch (codeStatement)
                    {
                        case IInstantiationStatement instantiationStatement:
                            yield return new TransformedInstantiationCodeStatement(
                                instantiationStatement,
                                _transformation
                            );
                            break;
                        default:
                            yield return codeStatement;
                            break;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            /*
            * @todo #98 60m/DEV Cover with tests.
            */

            return _transformation.TryToApplyTo(_origin, otherwise: x => this).ToJointString().ToString();
        }

        public ISyntaxTree Data => _origin;

        public Func<ISyntaxTree, string> FormattingStrategy { get; } = x => x.ToString();
    }

    public class TransformedInstantiationCodeStatement : IInstantiationStatement
    {
        private readonly IInstantiationStatement _codeStatement;
        private readonly ITransformation _transformation;

        public TransformedInstantiationCodeStatement(IInstantiationStatement codeStatement, ITransformation transformation)
        {
            _codeStatement = codeStatement;
            _transformation = transformation;
        }

        public override string ToString()
        {
            return _codeStatement.ToString();
        }

        public ITypeDefinition Type => _transformation.TryToApplyTo(_codeStatement.Type);

        public IArguments Arguments => _transformation.TryToApplyTo(_codeStatement.Arguments, x => new TransformedArguments(x, _transformation));

        public IPropertyAssignments InlinePropertiesAssignment => _transformation.TryToApplyTo(_codeStatement.InlinePropertiesAssignment, x => new TransformedPropertyAssignments(x, _transformation));
    }

    public interface IFormatted<T>
    {
        T Data { get; }

        Func<T, string> FormattingStrategy { get; }
    }

    public class TransformedArguments : IArguments
    {
        public TransformedArguments(IArguments origin, ITransformation transformation)
        {

        }

        public IEnumerator<IArgument> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TransformedPropertyAssignments : IPropertyAssignments
    {
        public TransformedPropertyAssignments(IPropertyAssignments origin, ITransformation transformation)
        {

        }

        public IEnumerator<IAssignmentPart> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ArgumentsWithSpacesBetween : IArguments, IFormatted<IArguments>
    {
        /*
        * @todo #98 60m/DEV Split hardcoded formatting rules into separate objects, prefere composition.
        */

        private readonly IFormatted<IArguments> _arguments;

        public ArgumentsWithSpacesBetween(IFormatted<IArguments> arguments)
        {
            _arguments = arguments;
        }

        public IEnumerator<IArgument> GetEnumerator()
        {
            return _arguments.Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IArguments Data => this;

        public Func<IArguments, string> FormattingStrategy => x => string.Join(", ", x.Select(x => x.ToString()));

    }
}