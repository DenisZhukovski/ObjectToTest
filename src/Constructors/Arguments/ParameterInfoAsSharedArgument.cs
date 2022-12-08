using System.Reflection;
using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    /// <summary>
    /// When constructor argument is a shared object the name and return type is not correct because of the custom
    /// object class. The class's name and type may not git the argument's name and type
    /// </summary>
    public class ParameterInfoAsSharedArgument : IArgument
    {
        private readonly IArgument _sharedArgument;
        private readonly ParameterInfo _parameter;

        public ParameterInfoAsSharedArgument(IArgument sharedArgument, ParameterInfo parameter)
        {
            _sharedArgument = sharedArgument;
            _parameter = parameter;
        }

        public string Name => _parameter.Name;

        public string Type => _parameter.ParameterType.TypeName();

        public object? Object => _sharedArgument.Object;

        public IConstructor Constructor => _sharedArgument.Constructor;

        public override bool Equals(object? obj)
        {
            return _sharedArgument.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _sharedArgument.GetHashCode();
        }

        public override string ToString()
        {
            return Constructor.ToString();
        }
    }
}