namespace ObjectToTest.UnitTests.Models
{
    public class WithCustomData
    {
        private readonly WithCustomHashCode _withCustomHashCode;

        public WithCustomData(WithCustomHashCode withCustomHashCode)
        {
            _withCustomHashCode = withCustomHashCode;
        }

        public WithCustomHashCode Foo()
        {
            return _withCustomHashCode;
        }
    }
}
