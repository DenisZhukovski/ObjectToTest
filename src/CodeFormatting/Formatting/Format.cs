using System;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class Format : IFormat, IExternalToStringDefinition, ITransformationDefinition
    {
        private readonly List<FormatAndCondition> _conditionalFormats = new();
        private readonly List<TransformationAndCondition> _conditionalTransformations = new();

        public void For<T>(string format, Func<T, Args> args)
        {
            _conditionalFormats.Add(new FormatAndCondition()
            {
                IsApplicable = x => x is T,
                Format = new ObjectWithFormat<T>(format, args)
            });
        }

        public void ForArrayOf<T>(Func<string[], string> format)
        {
            _conditionalFormats.Add(new FormatAndCondition()
            {
                IsApplicable = x => x is IEnumerable<T>,
                Format = new ArrayWithFormat<T[]>(format)
            });
        }

        public void OverrideForArrayOf<T>(Func<string[], string> format)
        {
            _conditionalFormats.Insert(
                0,
                new FormatAndCondition()
                {
                    IsApplicable = x => x is IEnumerable<T>,
                    Format = new ArrayWithFormat<T[]>(format)
                }
            );
        }

        public void If(Func<object, bool> condition, IObjectWithFormat format)
        {
            _conditionalFormats.Add(new FormatAndCondition()
            {
                IsApplicable = condition,
                Format = format
            });
        }

        public void If(Func<object, bool> isApplicable, Func<string, string> transform)
        {
            _conditionalTransformations.Add(new TransformationAndCondition()
            {
                IsApplicable = isApplicable,
                Format = transform
            });
        }

        public string ApplyTo(object root)
        {
            var results = new List<DataAndString>();

            return Resolve(root);

            string Resolve(object arg)
            {
                var potentialResult = results.FirstOrDefault(x => ReferenceEquals(x.Data, arg));

                if (potentialResult != null)
                {
                    return potentialResult.String;
                }

                var conditionalFormat = _conditionalFormats.FirstOrDefault(x => x.IsApplicable(arg));

                if (conditionalFormat != null)
                {
                    var chainOfTransformations = _conditionalTransformations.Where(x => x.IsApplicable(arg));

                    var format = conditionalFormat.Format.Format(arg);

                    foreach (var transformationAndCondition in chainOfTransformations)
                    {
                        format = transformationAndCondition.Format(format);
                    }

                    var result = new FormattedString(
                        format,
                        conditionalFormat.Format.Args(arg).Select(Resolve).ToArray()
                    );

                    results.Add(new DataAndString()
                    {
                        Data = arg,
                        String = result.ToString()
                    });

                    return result.ToString();
                }

                {
                    var result = arg.ToString();

                    results.Add(new DataAndString()
                    {
                        Data = arg,
                        String = result.ToString()
                    });

                    return result;
                }

            }
        }

        private record FormatAndCondition
        {
            public IObjectWithFormat Format { get; set; }

            public Func<object, bool> IsApplicable { get; set; }
        }

        private record TransformationAndCondition
        {
            public Func<string, string> Format { get; set; }

            public Func<object, bool> IsApplicable { get; set; }
        }

        private record DataAndString
        {
            public object Data { get; set; }

            public string String { get; set; }
        }
    }
}