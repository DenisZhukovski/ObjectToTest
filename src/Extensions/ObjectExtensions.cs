using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest
{
    public static class ObjectExtensions
    {
        public static string ToTest(this object @object)
        {
            var objectNameCounter = 1;

            return Extensions.ObjectToTest.GetResultStringForObject(@object, ref objectNameCounter, out _);
        }

        internal static string Join(this IList<object> arguments)
        {
            return string.Join(",\r\n", arguments.Select(arg => arg.ToTest()));
        }
    }
}
