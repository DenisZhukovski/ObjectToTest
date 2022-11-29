using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public interface IArgument
    {
        string Name { get;  }

        /// <summary>
        /// It an argument type name
        /// </summary>
        string Type { get; }
        
        object? Object { get; }
        
        IConstructor Constructor { get; }
    }
}
