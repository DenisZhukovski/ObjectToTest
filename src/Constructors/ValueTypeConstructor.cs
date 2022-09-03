using System;
using ObjectToTest.Arguments;
using System.Collections.Generic;

namespace ObjectToTest.Constructors
{
    internal class ValueTypeConstructor : IConstructor
    {
        private readonly object _object;

        public ValueTypeConstructor(object @object)
        {
            _object = @object;
        }

        public bool IsValid => true;

        public IList<IArgument> Argumetns => new List<IArgument>();

        public override bool Equals(object? obj)
        {
            return EqualityComparer<object?>.Default.Equals(_object, obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }

        public override string ToString()
        {
            return _object.ToStringForInialization();
        }
    }
}

