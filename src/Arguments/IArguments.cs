using System;
using System.Reflection;

namespace ObjectToTest.Arguments
{
    public interface IArguments
    {
        IArgument? Argument(object argument);
    }
}

