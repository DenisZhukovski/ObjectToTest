using System;
using ObjectToTest.Constructors;

namespace ObjectToTest.ConstructorParameters
{
    public interface IArgument
    {
        string Name { get;  }

        IConstructor Constructor();
    }
}
