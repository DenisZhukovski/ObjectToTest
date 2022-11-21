using System.Collections.Generic;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class SharedArgumentConstructor : IConstructor
    {
        private readonly IArgument _argument;
        private bool _initVariable;

        public SharedArgumentConstructor(IArgument argument)
        {
            _argument = argument;
        }

        bool IConstructor.IsValid => _argument.Constructor.IsValid;

        IList<IArgument> IConstructor.Arguments => _argument.Constructor.Arguments;

        public override bool Equals(object? obj)
        {
            return _argument.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _argument.GetHashCode();
        }

        public override string ToString()
        {
            if (!_initVariable)
            {
                _initVariable = true;
                return $"var {_argument.Name} = {_argument.Constructor}";
            }
            return _argument.Name;
        }
    }
}

