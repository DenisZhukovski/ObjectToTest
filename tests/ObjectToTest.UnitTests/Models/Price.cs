using System;
namespace ObjectToTest.UnitTests.Models
{
    public class Price : IPrice
    {
        private readonly decimal _price;

        public Price(decimal price)
        {
            _price = price;
        }

        public decimal ToDecimal()
        {
            return _price;
        }
    }
}
