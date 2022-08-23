using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal class StringParameter : ObjectConstructorParameter
    {
        public StringParameter(object @object, ParameterInfo parameterInfo)
            : base(@object, parameterInfo)
        {
        }

        public override string ToString()
        {
            var value = GetValueFromObject();

            if (value is null)
            {
                return "null";
            }

            return $"\"{value}\"";
        }
    }
}
