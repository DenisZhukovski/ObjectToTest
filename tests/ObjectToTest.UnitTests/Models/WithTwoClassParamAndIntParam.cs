namespace ObjectToTest.UnitTests.Models
{
    public class WithTwoClassParamAndIntParam
    {
        private readonly int _intParam;

        public WithTwoClassParamAndIntParam(
            WithClassParam withClassParam,
            WithClassAndIntParams withClassAndIntParams,
            int intParam)
        {
            WithClassParam = withClassParam;
            WithClassAndIntParams = withClassAndIntParams;
            _intParam = intParam;
        }

        public WithClassParam WithClassParam
        {
            get;
        }

        public WithClassAndIntParams WithClassAndIntParams
        {
            get;
        }
    }
}
