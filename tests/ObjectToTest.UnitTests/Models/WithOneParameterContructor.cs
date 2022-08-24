namespace ObjectToTest.UnitTests.Models
{
    public class WithOneParameterContructorAndPublicReadProperty
    {
        public WithOneParameterContructorAndPublicReadProperty(int intProperty)
        {
            IntProperty = intProperty;
        }

        public int IntProperty { get; }
    }
}
