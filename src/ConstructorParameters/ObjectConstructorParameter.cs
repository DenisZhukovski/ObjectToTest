using System;
using System.Linq;
using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal abstract class ObjectConstructorParameter : IArgument
    {
        protected readonly object _object;
        protected readonly ParameterInfo _parameter;

        protected ObjectConstructorParameter(object @object, ParameterInfo parameter)
        {
            _object = @object;
            _parameter = parameter;
        }

        public abstract override string ToString();

        protected object GetValueFromObject()
        {
            var type = _object.GetType();
            var fieldInfo = type
                .GetRuntimeFields()
                .FirstOrDefault(f => IsVariableNameEqual(f.Name, _parameter.Name));
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(_object);
            }
            else
            {
                var propertyInfo = type.
                    GetProperties()
                    .FirstOrDefault(p => IsVariableNameEqual(p.Name, _parameter.Name));
                if (propertyInfo != null)
                {
                    return propertyInfo.GetValue(_object);
                }
            }

            throw new ArgumentException($"Can not get value for parameter with name {_parameter.Name} in type {type.Name}");
        }

        protected static bool IsVariableNameEqual(string fromTypeName, string fromParamName)
        {
            fromTypeName = fromTypeName.Replace("_", string.Empty);
            fromParamName = fromParamName.Replace("_", string.Empty);
            return fromTypeName.Equals(fromParamName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
