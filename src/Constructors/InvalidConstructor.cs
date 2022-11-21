using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Xml.Linq;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class InvalidConstructor : IConstructor
    {
        private readonly object _object;
        private readonly ParameterInfo _parameter;

        public InvalidConstructor(object @object, ParameterInfo parameter)
        {
            _object = @object;
            _parameter = parameter;
        }

        public bool IsValid => false;

        public IList<IArgument> Arguments => new List<IArgument>();

        public override bool Equals(object? obj)
        {
            return (obj is IConstructor constructor && constructor.Equals(_object)) || _object.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }

        public override string ToString()
        {
            return $"No valid parameter was found for {_parameter.Name} in object: {_object}";
        }
    }
}

