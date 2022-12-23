using System;
using System.Collections.Generic;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class SharedArgumentConstructor : IConstructor
    {
        private readonly IArgument _argument;
        private readonly IArguments _sharedArguments;
        private bool _initVariable = false;

        public SharedArgumentConstructor(IArgument argument, IArguments sharedArguments)
        {
            _argument = argument;
            _sharedArguments = sharedArguments;
        }

        bool IConstructor.IsValid => _argument.Constructor.IsValid;

        IList<IArgument> IConstructor.Arguments => _argument.Constructor.Arguments;
        
        public object? Object => _argument.Constructor.Object;

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
                if (_argument.Object is Delegate @delegate)
                {
                    if (_sharedArguments.Contains(@delegate.Target))
                    {
                        return string.Empty;
                    }
                }
                return $"var {_argument.Name} = {_argument.Constructor}";
            }
            return _argument.Name;
        }
    }
}

