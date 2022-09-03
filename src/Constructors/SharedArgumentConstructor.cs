using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class SharedArgumentConstructor : IConstructor
    {
        private readonly IArgument _argument;
        private string _initVariable = string.Empty;

        public SharedArgumentConstructor(IArgument argument)
        {
            _argument = argument;
        }

        bool IConstructor.IsValid => _argument.Constructor.IsValid;

        IList<IArgument> IConstructor.Argumetns => _argument.Constructor.Argumetns;

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
            if (string.IsNullOrEmpty(_initVariable))
            {
                _initVariable = $"var {_argument.Name} = {_argument.Constructor}";
            }
            return _argument.Name;
        }
    }
}

