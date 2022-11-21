using System.Collections.Generic;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public interface IConstructor
    {
        bool IsValid { get; }

        IList<IArgument> Arguments { get; }
    }
}

