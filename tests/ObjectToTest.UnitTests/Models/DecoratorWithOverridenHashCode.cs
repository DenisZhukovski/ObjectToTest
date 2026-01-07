using System;

namespace ObjectToTest.UnitTests.Models
{
    public class DecoratorWithOverridenHashCode : IPrice
    {
        private readonly Price _price;

        public DecoratorWithOverridenHashCode(Price price)
        {
            _price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public decimal ToDecimal()
        {
            return _price.ToDecimal();
        }

        public override int GetHashCode()
        {
            // Force a deterministic hash code irrespective of the wrapped price instance.
            return 42;
        }
    }
}

