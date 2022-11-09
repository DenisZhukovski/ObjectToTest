namespace ObjectToTest.UnitTests.Models
{
    public class WithSingletonArgument
    {
        private readonly SingletonClass _singletonClass;

        public WithSingletonArgument(SingletonClass singletonClass)
        {
            _singletonClass = singletonClass;
        }
    }
}
