using ObjectToTest.Constructors;

namespace ObjectToTest
{
    public static class ConstructorExtensions
    {
        public static string Type(this IConstructor ctor)
        {
            return ctor.Object != null
                ? ctor.Object.GetType().TypeName()
                : "null";
        }
    }
}