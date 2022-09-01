using System;
using ObjectToTest.ConstructorParameters;

namespace ObjectToTest
{
    public class ObjectSharedArguments : IArguments
    {
        private readonly object _object;

        public ObjectSharedArguments(object @object)
        {
            _object = @object;
        }

        public IArgument? Argument(object argument)
        {
            return null;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}

