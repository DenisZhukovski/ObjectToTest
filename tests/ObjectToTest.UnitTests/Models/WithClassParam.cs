namespace ObjectToTest.UnitTests.Models
{
    public class WithClassParam
    {
        private EmptyObject _emptyObject;

        public WithClassParam(EmptyObject emptyObject)
        {
            _emptyObject = emptyObject;
        }
    }
}
