using System.Reflection;
using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public class ParameterInfoAsArgument : IArgument
    {
        private readonly ParameterInfo _parameterInfo;
        private readonly object _object;
        private readonly IArguments _sharedArguments;
        private IArgument? _argument;

        public ParameterInfoAsArgument(ParameterInfo parameterInfo, object @object, IArguments sharedArguments)
        {
            _parameterInfo = parameterInfo;
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public string Name => CachedArgument.Name;
        
        public string Type => CachedArgument.Type;
        
        public object? Object => CachedArgument.Object;
        
        public IConstructor Constructor => CachedArgument.Constructor;

        public override bool Equals(object? obj)
        {
            return CachedArgument.Equals(obj);
        }

        public override int GetHashCode()
        {
            return CachedArgument.GetHashCode();
        }

        public override string ToString()
        {
            return CachedArgument.ToString();
        }
        
        private IArgument CachedArgument => _argument ??= Argument(_parameterInfo);
        
        private IArgument Argument(ParameterInfo parameter)
        {
            if (_object.Contains(parameter))
            {
                var sharedArgument = _sharedArguments.Argument(_object.Value(parameter));
                if (sharedArgument != null)
                {
                    return new ParameterInfoAsSharedArgument(sharedArgument, parameter);
                }
            }

            return new Argument(
                parameter.Name,
                parameter.ParameterType,
                _object.Contains(parameter) 
                    ? _object.Value(parameter)
                    : parameter.Default(),
                ParameterConstructor(parameter)
            );
        }
        
        private IConstructor ParameterConstructor(ParameterInfo parameter)
        {
            if (_object.Contains(parameter))
            {
                return _object
                    .Value(parameter)
                    .Constructor(_sharedArguments);
            }
            return new InvalidConstructor(_object, _sharedArguments);
        }
    }
}