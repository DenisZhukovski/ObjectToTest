using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Arguments;
using ObjectToTest.Exceptions;

namespace ObjectToTest.Constructors
{
    public class InvalidConstructor : IConstructor
    {
        private readonly object _object;
        private readonly IArguments _sharedArguments;

        public InvalidConstructor(object @object)
            : this(@object, new ObjectSharedArguments(@object))
        {
        }
        
        public InvalidConstructor(object @object, IArguments sharedArguments)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public bool IsValid => false;

        public IList<IArgument> Arguments 
        {
            get
            {
                foreach (var ctor in _object.Constructors())
                {
                    var arguments = new List<IArgument>();
                    foreach (var parameter in ctor.GetParameters())
                    {
                        arguments.Add(MapParameter(parameter));
                    }

                    if (arguments.Any(a => !a.Constructor.IsValid))
                    {
                        return arguments;
                    }
                }

                return new List<IArgument>();
            }    
        }

        public object? Object => _object;

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
            throw new NoConstructorException(_object);
        }
        
        protected virtual IArgument MapParameter(ParameterInfo parameter)
        {
            if (_object.Contains(parameter))
            {
                var sharedArgument = _sharedArguments.Argument(_object.Value(parameter));
                if (sharedArgument != null)
                {
                    return sharedArgument;
                }
            }

            return new Argument(
                parameter.Name,
                parameter.ParameterType,
                _object.Contains(parameter) 
                    ? _object.Value(parameter)
                    : parameter.Default(),
                Constructor(parameter)
            );
        }

        private IConstructor Constructor(ParameterInfo parameter)
        {
            if (_object.Contains(parameter))
            {
                return _object
                    .Value(parameter)
                    .Constructor(_sharedArguments);
            }
            return new InvalidConstructor(_object, _sharedArguments);
        }
    }
}

