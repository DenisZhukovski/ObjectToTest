using System;

namespace ObjectToTest
{
    public static class ObjectExtensions
    {
        public static string ToXUnit(this object @object)
        {
            return new ObjectAsConstructor(@object).ToString();
        }
    }
}
