namespace ObjectToTest.UnitTests.Models
{
    public class WithTwoParamOneFieldAndOneProperty
    {
        private readonly int _intValue;

        public WithTwoParamOneFieldAndOneProperty(int intValue, string strValue)
        {
            _intValue = intValue;
            StrValue = strValue;
        }

        public string StrValue { get; }
    }
}
