using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public class SharedArgument : IArgument
    {
        private readonly IArgument _argument;
        private IConstructor? _constructor;

        public SharedArgument(IArgument argument)
        {
            _argument = argument;
        }

        public string Name => _argument.Name;

        public IConstructor Constructor => _constructor ??= new SharedArgumentConstructor(_argument);
       
        public object? Object => _argument.Object;

        public override bool Equals(object? obj)
        {
            return _argument.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _argument.GetHashCode();
        }

        public override string ToString()
        {
            return Constructor.ToString();
        }
    }
}

