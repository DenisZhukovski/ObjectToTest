﻿using ObjectToTest.Arguments;
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
            return _object.ToStringForInitialization();
        }
    }
}

