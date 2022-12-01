using ObjectToTest.Arguments;
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

        public bool IsValid => Arguments.All(a => _object.Contains(a.Name) && a.Constructor.IsValid);

        public IList<IArgument> Arguments => _arguments ??= _constructor
            .GetParameters()
            .Select(parameterInfo => new ParameterInfoAsArgument(parameterInfo, _object, _sharedArguments))
            .ToList<IArgument>();

        public object? Object => _object;

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
            var paramsStr = string.Join(",", Arguments);
            var objectType = _object.GetType();
            if (objectType.IsGenericType)
            {
                return $"new {objectType.GenericTypeName()}({paramsStr}){new ObjectProperties(_object, _sharedArguments)}";
            }
            return $"new {objectType.Name}({paramsStr}){new ObjectProperties(_object, _sharedArguments)}";
        }
    }
}
