namespace ObjectToTest.UnitTests.Models
{
    public class WithSingletonAndOtherArgument
    {
        private readonly SingletonClass _singletonClass;
        private readonly IPrice _price;

        public WithSingletonAndOtherArgument(SingletonClass singletonClass, IPrice price)
        {
            _singletonClass = singletonClass;
            _price = price;
        }
    }
}
