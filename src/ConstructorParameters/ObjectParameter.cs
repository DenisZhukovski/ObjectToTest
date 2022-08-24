using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal class ObjectParameter : ObjectConstructorParameter, IArgument
    {
        public ObjectParameter(object @object, ParameterInfo parameter)
            : base(@object, parameter)
        {
        }

        public override string ToString()
        {
            var value = GetValueFromObject();
            return value is null
                ? "null"
                : new ObjectAsConstructor(value).ToString();
        }
    }
}
