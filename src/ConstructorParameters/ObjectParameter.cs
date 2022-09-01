using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal class ObjectParameter : ObjectConstructorParameter
    {
        public ObjectParameter(object @object, ParameterInfo parameter, IArguments sharedArguments)
            : base(@object, parameter, sharedArguments)
        {
        }

        public override string ToString()
        {
            var value = _object.Value(_parameter);
            return value is null
                ? "null"
                : new ObjectAsConstructor(value).ToString();
        }
    }
}
