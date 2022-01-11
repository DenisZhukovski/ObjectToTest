namespace ObjectToTest.UnitTests.Data
{
    public class Foo
    {
        private readonly IPrice _price;
        private readonly IUser _user;

        public Foo(IPrice price, IUser user)
        {
            _price = price;
            _user = user;
        }

        public string Hello()
        {
            return "Hello" + _price.ToDecimal() + _user.ToString();
        }
    }
}
