namespace ObjectToTest.UnitTests.Models;

public class DecoratorWithOverridenHashCode : IPrice
{
    private readonly Price _price;

    public DecoratorWithOverridenHashCode(Price price)
    {
        _price = price;
    }
    
    public decimal ToDecimal()
    {
        return _price.ToDecimal() + 10; 
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || _price.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _price.GetHashCode();
    }
}