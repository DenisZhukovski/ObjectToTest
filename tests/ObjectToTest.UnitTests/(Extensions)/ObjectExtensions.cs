using System;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public static class ObjectExtensions
    {
        public static T Log<T>(this T item, ITestOutputHelper output)
        {
            output.WriteLine(Convert.ToString(item));
            return item;
        }
    }
}