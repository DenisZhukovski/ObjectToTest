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
        
        public static string ToTest(this object item, ITestOutputHelper output)
        {
            return item.ToTest().Log(output);
        }
        
        public static string ToTestWellFormatted(this object item, ITestOutputHelper output)
        {
            return item.ToTestWellFormatted().Log(output);
        }
        
        public static string ToString(this object item, ITestOutputHelper output)
        {
            return item.ToString().Log(output);
        }

        public static T CastTo<T>(this object item) => (T) item;
    }
}