using System;

namespace ObjectToTest.UnitTests.Models
{
    public class InheritedBase
    {
        protected readonly Price _price;

        protected InheritedBase(Price price)
        {
            _price = price ?? throw new ArgumentNullException(nameof(price));
        }
    }

    public class InheritedClass : InheritedBase
    {
        public InheritedClass(Price price)
            : base(price)
        {
        }

        public InheritedClass(int amount)
            : this(new Price(amount))
        {
        }
    }
}

