namespace ObjectToTest.UnitTests.Models;

public class InheritedClass : SecondaryConstructor
{
    public InheritedClass(decimal priceInEuro)
        : base(priceInEuro)
    {
    }

    public InheritedClass(IPrice price)
        : base(price)
    {
    }
}