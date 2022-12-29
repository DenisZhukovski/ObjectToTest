using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class ArrayWithFormat<T> : IObjectWithFormat where T : IEnumerable
    {
        private readonly Func<string[], Tabs, (string, Tabs)> _format;

        public ArrayWithFormat(Func<string[], Tabs, (string, Tabs)> format)
        {
            _format = format;
        }

        public (string, Tabs) Format(object item, Tabs tabs)
        {
            if (item is IEnumerable expected)
            {
                var result = new List<int>();
                var i = 0;
                foreach (var expectedItem in expected)
                {
                    result.Add(i);
                    i++;
                }

                var indexes = result.Select(index => "{" + index + "}").ToArray();

                return _format(indexes, tabs);
            }

            return ("", tabs);
        }

        public object[] Args(object item)
        {
            if (item is IEnumerable expectedItems)
            {
                var result = new List<object>();
                foreach (var expectedItem in expectedItems)
                {
                    result.Add(expectedItem);
                }

                return result.ToArray();
            }

            return Array.Empty<object>();
        }

        public override string ToString()
        {
            return $"ArrayWithFormat<{typeof(T).Name}>";
        }
    }
}