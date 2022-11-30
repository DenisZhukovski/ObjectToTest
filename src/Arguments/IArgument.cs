using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public interface IArgument
    {
        string Name { get;  }

        /// <summary>
        /// It an object type name which is encapsulated by argument 
        /// </summary>
        string Type { get; }
        
        object? Object { get; }
        
        IConstructor Constructor { get; }
    }
}
