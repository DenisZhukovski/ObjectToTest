using System;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class ObjectWithFormat<T> : IObjectWithFormat
    {
        private readonly string _format;
        private readonly Func<T, Args> _args;

        public ObjectWithFormat(string format, Func<T, Args> args)
        {
            _format = format;
            _args = args;
        }

        public (string, Tabs) Format(object item, Tabs tabs)
        {
            return (_format, tabs);
        }

        public object[] Args(object item)
        {
            if (item is T expected)
            {
                return _args(expected).ToArray();
            }

            return new object[0];
        }
    }
}