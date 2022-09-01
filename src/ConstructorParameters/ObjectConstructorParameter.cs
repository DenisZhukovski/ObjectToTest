using System;
using System.Linq;
using System.Reflection;
using ObjectToTest.Constructors;
using ObjectToTest.Extensions;

namespace ObjectToTest.ConstructorParameters
{
    internal abstract class ObjectConstructorParameter : IArgument
    {
        protected readonly object _object;
        protected readonly ParameterInfo _parameter;
        private readonly IArguments _sharedArguments;

        protected ObjectConstructorParameter(
            object @object,
            ParameterInfo parameter,
            IArguments sharedArguments)
        {
            _object = @object;
            _parameter = parameter;
            _sharedArguments = sharedArguments;
        }

        public string Name => _parameter.Name;

        public virtual IConstructor Constructor()
        {
            var parameterObject = _object.Value(_parameter);
            return parameterObject?.ValidConstructor(_sharedArguments) ?? throw new InvalidOperationException("Not possible to find a constructor for null");
        }

        public abstract override string ToString();
    }
}
