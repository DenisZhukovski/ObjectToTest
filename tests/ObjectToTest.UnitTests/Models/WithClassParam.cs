namespace ObjectToTest.UnitTests.Models
{
    public class WithClassParam
    {
        private readonly EmptyObject _emptyObject;

        public WithClassParam(EmptyObject emptyObject)
        {
            _emptyObject = emptyObject;
        }
    }
}
