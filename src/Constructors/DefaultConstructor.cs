using System.Collections.Generic;
using System.Linq;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    internal class DefaultConstructor : IConstructor
    {
        private readonly object _object;
        private readonly IArguments _sharedArguments;

        public DefaultConstructor(object @object, IArguments sharedArguments)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public bool IsValid => _object
                .GetType()
                .GetConstructors()
                .Any(c => !c.GetParameters().Any() && c.IsPublic);

        public IList<IArgument> Argumetns => new List<IArgument>();

        public override bool Equals(object? obj)
        {
            return EqualityComparer<object?>.Default.Equals(_object, obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }

        public override string ToString()
        {
            var objectType = _object.GetType();
            if (objectType.IsGenericType)
            {
                return $"new {objectType.GenericTypeName()}(){new ObjectProperties(_object, _sharedArguments)}";
            }
            return $"new {objectType.Name}(){new ObjectProperties(_object, _sharedArguments)}";
        }
    }
}
