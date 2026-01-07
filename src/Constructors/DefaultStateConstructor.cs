using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    /// <summary>
    /// Represents a constructor call derived from default-state analysis (IL-driven).
    /// It bypasses argument discovery on the current object and uses precomputed default arguments.
    /// </summary>
    internal class DefaultStateConstructor : IConstructor
    {
        private readonly object _object;
        private readonly ConstructorInfo _constructorInfo;
        private readonly object?[] _arguments;
        private readonly IArguments _sharedArguments;

        public DefaultStateConstructor(
            object @object,
            ConstructorInfo constructorInfo,
            object?[] arguments,
            IArguments sharedArguments)
        {
            _object = @object;
            _constructorInfo = constructorInfo;
            _arguments = arguments;
            _sharedArguments = sharedArguments;
        }

        public bool IsValid => true;

        public IList<IArgument> Arguments => new List<IArgument>();

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
            var paramsStr = string.Join(
                ",",
                _arguments.Select(arg => arg.ToStringForInitialization())
            );
            var objectType = _object.GetType();
            var typeName = objectType.IsGenericType ? objectType.GenericTypeName() : objectType.Name;
            var ctorParametersExist = _constructorInfo.GetParameters().Any();
            var argsPart = ctorParametersExist ? paramsStr : string.Empty;
            return $"new {typeName}({argsPart}){new ObjectProperties(_object, _sharedArguments)}";
        }
    }
}

