using System;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public static class FormatExtensions
    {
        public static void For<T>(this IFormat self, string format, Func<T, Args> args, string name = null)
        {
            self.Add(new NodeFormat()
            {
                IsApplicable = x => x is T,
                Format = new ObjectWithFormat<T>(format, args),
                Name = name
            });
        }

        /// <summary>
        /// Transformation applied only once.
        /// i.e. '{0},{1}' and then '{0};{1}', only latest '{0};{1}' would be applied.
        /// Implicit type check is presented.
        /// </summary>
        public static void ForArrayOf<T>(this IFormat self, Func<string[], string> format, string name = null)
        {
            self.Add(new NodeFormat
            {
                IsApplicable = x => x is IEnumerable<T>,
                Format = new ArrayWithFormat<T[]>((x, tabs) => (format(x), tabs)),
                Name = name
            });
        }

        /// <summary>
        /// Transformation applied only once.
        /// i.e. '{0},{1}' and then '{0};{1}', only latest '{0};{1}' would be applied.
        /// Scope of appliance should be managed by condition from most abstract to less abstract.
        /// </summary>
        public static void OverrideForArrayOf<T>(this IFormat self, Func<object, bool> condition, Func<string[], string> format, string name = null)
        {
            self.AddAsFirst(
                new NodeFormat()
                {
                    IsApplicable = x => x is IEnumerable<T> && condition(x),
                    Format = new ArrayWithFormat<T[]>((x, tabs) => (format(x), tabs)),
                    Name = name
                }
            );
        }


        /// <summary>
        /// Transformation applied only once.
        /// i.e. '{0},{1}' and then '{0};{1}', only latest '{0};{1}' would be applied.
        /// Scope of appliance should be managed by condition from most abstract to less abstract.
        ///
        /// Tabs as an incoming parameters - tabs of parent.
        /// Tabs as an outgoing parameters - tabs of elements children.
        /// </summary>
        public static void OverrideForArrayOf<T>(this IFormat self, Func<object, bool> condition, Func<string[], Tabs, (string, Tabs)> format, string name = null)
        {
            self.AddAsFirst(
                new NodeFormat()
                {
                    IsApplicable = x => x is IEnumerable<T> && condition(x),
                    Format = new ArrayWithFormat<T[]>(format),
                    Name = name
                }
            );
        }

        /// <summary>
        /// Transformation applied only once.
        /// i.e. '{0},{1}' and then '{0};{1}', only latest '{0};{1}' would be applied.
        /// Scope of appliance should be managed by condition from most abstract to less abstract.
        /// </summary>
        public static void OverrideForArrayOf<T>(this IFormat self, Func<string[], string> format, string name = null)
        {
            self.AddAsFirst(
                new NodeFormat()
                {
                    IsApplicable = x => x is IEnumerable<T>,
                    Format = new ArrayWithFormat<T[]>((x, tabs) => (format(x), tabs)),
                    Name = name
                }
            );
        }

        /// <summary>
        /// Transformation is applied in stack for each applicable item.
        /// i.e. '{0}' => ' {0}' and then '{0}' => '-{0}', result would be '- {0}'.
        /// </summary>
        public static void If(this IFormat self, Func<object, bool> condition, IObjectWithFormat format, string name = null)
        {
            self.Add(new NodeFormat()
            {
                IsApplicable = condition,
                Format = format,
                Name = name
            });
        }

        /// <summary>
        /// Transformation is applied in stack for each applicable item.
        /// i.e. '{0}' => ' {0}' and then '{0}' => '-{0}', result would be '- {0}'.
        /// </summary>
        public static void If(this IFormat self, Func<object, bool> isApplicable, Func<string, string> transform, string name = null)
        {
            self.Add(new NodeTransformation()
            {
                IsApplicable = isApplicable,
                Format = (x, parentTabs) => (transform(x), parentTabs),
                Name = name
            });
        }

        /// <summary>
        /// Transformation is applied in stack for each applicable item.
        /// i.e. '{0}' => ' {0}' and then '{0}' => '-{0}', result would be '- {0}'.
        ///
        /// Tabs as an incoming parameters - tabs of parent.
        /// Tabs as an outgoing parameters - tabs of elements children.
        /// </summary>
        public static void If(this IFormat self, Func<object, bool> isApplicable, Func<string, Tabs, (string, Tabs)> transform, string name = null)
        {
            self.Add(new NodeTransformation()
            {
                IsApplicable = isApplicable,
                Format = transform,
                Name = name
            });
        }
    }
}