using System.Collections.Generic;
using ObjectToTest.Arguments;
using ObjectToTest.Exceptions;

namespace ObjectToTest.Constructors
{
    public class InvalidConstructor : IConstructor
    {
        private readonly object _object;

        public InvalidConstructor(object @object)
        {
            _object = @object;
        }

        public bool IsValid => false;

        public IList<IArgument> Arguments => new List<IArgument>();

        public object? Object => _object;

        public override bool Equals(object? obj)
        {
            return (obj is IConstructor constructor && constructor.Equals(_object)) || _object.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }

        public override string ToString()
        {
            throw new NoConstructorException(_object);
        }
    }
}

