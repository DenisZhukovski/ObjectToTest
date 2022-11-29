using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public interface IArgument
    {
        string Name { get;  }

        string Type { get; }
        
        object? Object { get; }
        
        IConstructor Constructor { get; }
    }
}
