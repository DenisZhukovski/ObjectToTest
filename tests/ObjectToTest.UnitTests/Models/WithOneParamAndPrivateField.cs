namespace ObjectToTest.UnitTests.Models
{
    public class WithOneParamAndPrivateField
    {
        private int _field;

        public WithOneParamAndPrivateField(int field)
        {
            _field = field;
        }
    }
}
