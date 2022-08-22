using System;

namespace ObjectToTest
{
    public static class ObjectExtensions
    {
        public static string ToXUnit(this object @object)
        {
            return new ObjectAsConstructor(@object).ToString();
        }

        public static string ToTest(this object @object)
        {
            var objectNameCounter = 1;

            return Extensions.ObjectToTest.GetResultStringForObject(@object, ref objectNameCounter, out _);
        }
    }
}
