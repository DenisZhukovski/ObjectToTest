namespace ObjectToTest.UnitTests.Models;

public class SecondaryConstructor
{
    private readonly IPrice _price;

    public SecondaryConstructor(decimal priceInEuro)
        : this(new Price(priceInEuro))
    {
        
    }
    public SecondaryConstructor(IPrice price)
    {
        _price = price;
    }

    public decimal Foo()
    {
        return _price.ToDecimal();
    }
}
    
