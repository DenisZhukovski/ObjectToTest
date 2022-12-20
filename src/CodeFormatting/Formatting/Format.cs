using System;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class Format : IFormat, IExternalToStringDefinition, ITransformationDefinition
    {
        /*
        * @todo #125 60m/DEV Use Insert 0 instead of add.
         *
         * Logic should be - latest format is (most likely) less abstract and applicable for shorter range of entities, so it should be analyzed first.
        */

        /*
        * @todo #125 60m/DEV Rename Override to just ForArrayOf.
         *
         * ...because there should be no knowledge about internals. Pure declarative style.
         *
        */

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
            _conditionalFormats.Add(new FormatAndCondition
            {
                IsApplicable = x => x is IEnumerable<T>,
                Format = new ArrayWithFormat<T[]>((x, tabs) => (format(x), tabs))
            });
        }

        public void OverrideForArrayOf<T>(Func<object, bool> condition, Func<string[], string> format)
        {
            _conditionalFormats.Insert(
                0,
                new FormatAndCondition()
                {
                    IsApplicable = x => x is IEnumerable<T> && condition(x),
                    Format = new ArrayWithFormat<T[]>((x, tabs) => (format(x), tabs))
                }
            );
        }

        public void OverrideForArrayOf<T>(Func<object, bool> condition, Func<string[], Tabs, (string, Tabs)> format)
        {
            _conditionalFormats.Insert(
                0,
                new FormatAndCondition()
                {
                    IsApplicable = x => x is IEnumerable<T> && condition(x),
                    Format = new ArrayWithFormat<T[]>(format)
                }
            );
        }

        public void OverrideForArrayOf<T>(Func<string[], string> format)
        {
            _conditionalFormats.Insert(
                0,
                new FormatAndCondition()
                {
                    IsApplicable = x => x is IEnumerable<T>,
                    Format = new ArrayWithFormat<T[]>((x, tabs) => (format(x), tabs))
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
                Format = (x, parentTabs) => (transform(x), parentTabs)
            });
        }

        public void If(Func<object, bool> isApplicable, Func<string, Tabs, (string, Tabs)> transform)
        {
            _conditionalTransformations.Add(new TransformationAndCondition()
            {
                IsApplicable = isApplicable,
                Format = transform
            });
        }

        public string ApplyTo(object item)
        {
            var results = new List<DataAndString>();
            return Resolve(item, new Tabs(0));
            string Resolve(object arg, Tabs parentTabs)
            {
                var potentialResult = results.FirstOrDefault(x => ReferenceEquals(x.Data, arg));
                if (potentialResult != null)
                {
                    return potentialResult.String;
                }

                var conditionalFormat = _conditionalFormats.FirstOrDefault(x => x?.IsApplicable(arg) ?? false);
                if (conditionalFormat != null)
                {
                    var chainOfTransformations = _conditionalTransformations.Where(x => x?.IsApplicable(arg) ?? false);
                    var (format, tabs) = conditionalFormat.Format.Format(arg, parentTabs);

                    foreach (var transformationAndCondition in chainOfTransformations)
                    {
                        (format, tabs) = transformationAndCondition.Format(format, tabs);
                    }

                    var result = new FormattedString(
                        format,
                        conditionalFormat.Format.Args(arg).Select(x => Resolve(x, tabs)).ToArray()
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

        private sealed record FormatAndCondition
        {
            public IObjectWithFormat? Format { get; set; }

            public Func<object, bool>? IsApplicable { get; set; }
        }

        private sealed record TransformationAndCondition
        {
            public Func<string, Tabs, (string, Tabs)>? Format { get; set; }

            public Func<object, bool>? IsApplicable { get; set; }
        }

        private sealed record DataAndString
        {
            public object? Data { get; set; }

            public string String { get; set; }
        }
    }
}