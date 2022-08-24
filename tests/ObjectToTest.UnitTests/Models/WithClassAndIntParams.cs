namespace ObjectToTest.UnitTests.Models
{
    public class WithClassAndIntParams
    {
        private readonly int _intField;

        public WithClassAndIntParams(int intField, EmptyObject emptyObject)
        {
            _intField = intField;
            EmptyObject = emptyObject;
        }

        public EmptyObject EmptyObject { get; }
    }
}
