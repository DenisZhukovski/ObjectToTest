using System;
using System.Linq;
using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal abstract class ObjectConstructorParameter
    {
        protected readonly object _object;
        protected readonly ParameterInfo _parameter;

        public ObjectConstructorParameter(object @object, ParameterInfo parameter)
        {
            _object = @object;
            _parameter = parameter;
        }

        public abstract override string ToString();

        protected object GetValueFromObject()
        {
            var type = _object.GetType();
            var typeProperties = type.GetProperties();
            var typeFields = type.GetRuntimeFields();

            var propertyInfo = typeProperties.FirstOrDefault(p => IsVariableNameEqual(p.Name, _parameter.Name));
            var fieldInfo = typeFields.FirstOrDefault(f => IsVariableNameEqual(f.Name, _parameter.Name));

            if (fieldInfo != null)
            {
                var fieldValue = fieldInfo.GetValue(_object);

                return fieldValue;
            }
            else if (propertyInfo != null)
            {
                var propertyValue = propertyInfo.GetValue(_object);

                return propertyValue;
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
