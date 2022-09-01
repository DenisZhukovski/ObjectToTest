using ObjectToTest.ConstructorParameters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectToTest.Constructors
{
    internal class ParameterizedConstructor : IConstructor
    {
        private readonly object _object;
        private readonly ConstructorInfo _constructor;
        private readonly IArguments _sharedArguments;
        private List<IArgument>? _arguments;

        public ParameterizedConstructor(object @object, ConstructorInfo constructor, IArguments sharedArguments)
        {
            _object = @object;
            _constructor = constructor;
            _sharedArguments = sharedArguments;
        }

        public bool IsValid => Argumetns.All(a => _object.Contains(a.Name) && a.Constructor().IsValid);

        public IList<IArgument> Argumetns => _arguments ??= _constructor
            .GetParameters()
            .Select(MapParameter)
            .ToList();

        public override string ToString()
        {
            var paramsStr = string.Join(",", Argumetns);
            var objectType = _object.GetType();
            if (objectType.IsGenericType)
            {
                return $"new {objectType.GenericTypeName()}({paramsStr})";
            }
            return $"new {objectType.Name}({paramsStr})";
        }

        protected virtual IArgument MapParameter(ParameterInfo parameter)
        {
            var sharedArgument = _sharedArguments.Argument(parameter.Member);
            if (sharedArgument != null)
            {
                return sharedArgument;
            }
           
            if (parameter.ParameterType.IsPrimitive || parameter.ParameterType == typeof(decimal))
            {
                return new SimpleTypeParameter(_object, parameter, _sharedArguments);
            }
            else if (parameter.ParameterType == typeof(string))
            {
                return new StringParameter(_object, parameter, _sharedArguments);
            }
            else if (parameter.ParameterType.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                return new CollectionArgument(_object, parameter, _sharedArguments);
            }
            else
            {
                return new ObjectParameter(_object, parameter, _sharedArguments);
            }
        }
    }
}
