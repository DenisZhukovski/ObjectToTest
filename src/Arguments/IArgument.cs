using System;
using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public interface IArgument
    {
        string Name { get;  }

        IConstructor Constructor { get; }
        
        object? Object { get; }
    }
}
