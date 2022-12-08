namespace ObjectToTest.UnitTests.Models
{
    public class WithOneParameterConstructorAndPublicReadProperty
    {
        public WithOneParameterConstructorAndPublicReadProperty(int intProperty)
        {
            IntProperty = intProperty;
        }

        public int IntProperty { get; }
    }
}
