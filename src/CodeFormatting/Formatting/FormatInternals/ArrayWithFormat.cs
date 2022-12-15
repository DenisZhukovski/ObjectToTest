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
                foreach (var expecteItem in expected)
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
            if (item is IEnumerable expected)
            {
                var result = new List<object>();
                foreach (var expecteItem in expected)
                {
                    result.Add(expecteItem);
                }

                return result.ToArray();
            }

            return new object[0];
        }
    }
}