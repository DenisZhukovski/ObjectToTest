using System;

namespace ObjectToTest.UnitTests.Models
{
    public class SecondaryConstructor : IPrice
    {
        private readonly Price _price;

        public SecondaryConstructor(Price price)
        {
            _price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public SecondaryConstructor(int amount)
            : this(new Price(amount))
        {
        }

        public decimal ToDecimal()
        {
            return _price.ToDecimal();
        }
    }
}

