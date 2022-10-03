using System.Collections.Generic;

namespace ObjectToTest.Arguments
{
    public interface IArguments
    {
        IArgument? Argument(object argument);
        
        List<IArgument> ToList();
    }

    public class MockArguments : IArguments
    {
        public IArgument? Argument(object argument)
        {
            return null;
        }

        public List<IArgument> ToList()
        {
            return new List<IArgument>();
        }
    }
}

