using System.Globalization;
using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class ComplexObjectWithInvalidArguments
    {
        private readonly IUser _user;
        private readonly IPrice _price;

        public ComplexObjectWithInvalidArguments(IUser user, IPrice price)
        {
            _user = user;
            _price = price;
        }

        public string Foo()
        {
            return _user + _price.ToDecimal().ToString(CultureInfo.InvariantCulture);
        }
    }
}