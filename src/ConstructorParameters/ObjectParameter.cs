using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal class ObjectParameter : ObjectConstructorParameter
    {
        public ObjectParameter(object @object, ParameterInfo parameter)
            : base(@object, parameter)
        {
        }

        public override string ToString()
        {
            var value = GetValueFromObject();

            if (value is null)
            {
                return "null";
            }

            var objectAsConstructor = new ObjectAsConstructor(value);

            return objectAsConstructor.ToString();
        }
    }
}
