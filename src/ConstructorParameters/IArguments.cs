using System;
using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    public interface IArguments
    {
        IArgument? Argument(object argument);
    }
}

