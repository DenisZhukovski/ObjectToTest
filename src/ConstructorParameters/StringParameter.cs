using System.Reflection;

namespace ObjectToTest.ConstructorParameters
{
    internal class StringParameter : ObjectConstructorParameter
    {
        public StringParameter(object @object, ParameterInfo parameterInfo, IArguments sharedArguments)
            : base(@object, parameterInfo, sharedArguments)
        {
        }

        public override string ToString()
        {
            var value = _object.Value(_parameter);
            return value is null
                ? "null"
                : $"\"{value}\""; 
        }
    }
}
