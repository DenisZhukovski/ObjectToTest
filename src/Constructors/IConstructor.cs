using System;
using System.Collections.Generic;
using ObjectToTest.ConstructorParameters;

namespace ObjectToTest.Constructors
{
    public interface IConstructor
    {
        bool IsValid { get;  }

        IList<IArgument> Argumetns { get;  }
    }
}

