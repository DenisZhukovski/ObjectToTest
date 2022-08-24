using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal class StringParameter : ObjectConstructorParameter, IArgument
    {
        public StringParameter(object @object, ParameterInfo parameterInfo)
            : base(@object, parameterInfo)
        {
        }

        public override string ToString()
        {
            var value = GetValueFromObject();
            return value is null
                ? "null"
                : $"\"{value}\""; 
        }
    }
}
