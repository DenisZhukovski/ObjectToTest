namespace ObjectToTest.UnitTests.Models
{
    public class WithClassParamThatDependsOnClass
    {
        private readonly WithClassParam _withClassParam;

        public WithClassParamThatDependsOnClass(WithClassParam withClassParam)
        {
            _withClassParam = withClassParam;
        }
    }
}
