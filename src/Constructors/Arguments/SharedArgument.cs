using ObjectToTest.Constructors;

namespace ObjectToTest.Arguments
{
    public class SharedArgument : IArgument
    {
        private readonly IArgument _argument;
        private readonly IArguments _sharedArguments;
        private IConstructor? _constructor;

        public SharedArgument(IArgument argument)
            : this(argument, new MockArguments())
        {
        }
        
        public SharedArgument(IArgument argument, IArguments sharedArguments)
        {
            _argument = argument;
            _sharedArguments = sharedArguments;
        }

        public string Name => _argument.Name;
        
        public string Type => _argument.Type;
        
        public object? Object => _argument.Object;

        public IConstructor Constructor => _constructor ??= new SharedArgumentConstructor(_argument, _sharedArguments);
       
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

