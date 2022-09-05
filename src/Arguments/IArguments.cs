using System;
using System.Reflection;

namespace ObjectToTest.Arguments
{
    public interface IArguments
    {
        IArgument? Argument(object argument);
    }

    public class MockArguments : IArguments
    {
        public IArgument? Argument(object argument)
        {
            return null;
        }
    }
}

