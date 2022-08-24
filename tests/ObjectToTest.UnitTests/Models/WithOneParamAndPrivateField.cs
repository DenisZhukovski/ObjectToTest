namespace ObjectToTest.UnitTests.Models
{
    public class WithOneParamAndPrivateField
    {
        private readonly int _field;

        public WithOneParamAndPrivateField(int field)
        {
            _field = field;
        }
    }
}
