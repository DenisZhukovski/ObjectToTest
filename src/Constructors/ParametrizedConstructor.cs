using ObjectToTest.Arguments;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectToTest.Constructors
{
    internal class ParametrizedConstructor : IConstructor
    {
        private readonly object _object;
        private readonly ConstructorInfo _constructor;
        private readonly IArguments _sharedArguments;
        private List<IArgument>? _arguments;

        public ParametrizedConstructor(object @object, ConstructorInfo constructor, IArguments sharedArguments)
        {
            _object = @object;
            _constructor = constructor;
            _sharedArguments = sharedArguments;
        }

        public bool IsValid => Argumetns.All(a => _object.Contains(a.Name) && a.Constructor.IsValid);

        public IList<IArgument> Argumetns => _arguments ??= _constructor
            .GetParameters()
            .Select(MapParameter)
            .ToList();

        public override bool Equals(object? obj)
        {
            return (obj is IConstructor constructor && constructor.Equals(_object))
                || _object.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }

        public override string ToString()
        {
            var paramsStr = string.Join(",", Argumetns);
            var objectType = _object.GetType();
            if (objectType.IsGenericType)
            {
                return $"new {objectType.GenericTypeName()}({paramsStr}){new ObjectProperties(_object, _sharedArguments)}";
            }
            return $"new {objectType.Name}({paramsStr}){new ObjectProperties(_object, _sharedArguments)}";
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
                _object.Contains(parameter) 
                    ? _object.Value(parameter)
                    : null,
                Constructor(parameter)
            );
        }

        private IConstructor Constructor(ParameterInfo parameter)
        {
            if (_object.Contains(parameter))
            {
                return _object
                    .Value(parameter)
                    .ValidConstructor(_sharedArguments);
            }
            return new InvalidConstructor(_object, parameter);
        }
    }
}
