using ObjectToTest.ConstructorParameters;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace ObjectToTest.Constructors
{
    internal class ParameterizedConstructor : IConstructor
    {
        private readonly object _object;
        private readonly ParameterInfo[] _parameters;

        public ParameterizedConstructor(object @object, ParameterInfo[] parameters)
        {
            _object = @object;
            _parameters = parameters;
        }

        public override string ToString()
        {
            var paramsStr = string.Join(",", _parameters.Select(MapParameter));
            var objectType = _object.GetType();
            if (objectType.IsGenericType)
            {
                return $"new {objectType.GenericTypeName()}({paramsStr})";
            }
            return $"new {objectType.Name}({paramsStr})";
        }

        protected virtual IArgument MapParameter(ParameterInfo parameter)
        {
            if (parameter.ParameterType.IsPrimitive || parameter.ParameterType == typeof(decimal))
            {
                return new SimpleTypeParameter(_object, parameter);
            }
            else if (parameter.ParameterType == typeof(string))
            {
                return new StringParameter(_object, parameter);
            }
            else if (parameter.ParameterType.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                return new CollectionArgument(_object, parameter);
            }
            else
            {
                return new ObjectParameter(_object, parameter);
            }
        }
    }
}
