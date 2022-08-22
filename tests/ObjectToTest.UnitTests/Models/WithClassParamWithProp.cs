namespace ObjectToTest.UnitTests.Models
{
    public class WithClassParamWithProp
    {
        private readonly int _intField;

        public WithClassParamWithProp(WithOnePublicProperty withOnePublicProperty, int intField)
        {
            WithOnePublicProperty = withOnePublicProperty;
            _intField = intField;
        }

        public WithOnePublicProperty WithOnePublicProperty { get; }
    }
}
