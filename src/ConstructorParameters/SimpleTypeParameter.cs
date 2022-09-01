using System;
using System.Reflection;
using ObjectToTest.Constructors;

namespace ObjectToTest.ConstructorParameters
{
    internal class SimpleTypeParameter : ObjectConstructorParameter
    {
        public SimpleTypeParameter(
            object @object,
            ParameterInfo parameter,
            IArguments sharedArguments)
            : base(@object, parameter, sharedArguments)
        {
        }

        public override IConstructor Constructor()
        {
            return  new ValueTypeConstructor(_object);
        }

        public override string ToString()
        {
            return _object.Value(_parameter)?.ToString() ?? "null";
        }
    }
}
