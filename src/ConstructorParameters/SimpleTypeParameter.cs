using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal class SimpleTypeParameter : ObjectConstructorParameter, IArgument
    {
        public SimpleTypeParameter(object @object, ParameterInfo parameter)
            : base(@object, parameter)
        {
        }

        public override string ToString()
        {
            var value = GetValueFromObject();

            return value.ToString();
        }
    }
}
