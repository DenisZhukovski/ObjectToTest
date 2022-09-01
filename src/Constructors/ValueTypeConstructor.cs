using System;
using ObjectToTest.ConstructorParameters;
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

        public override string ToString()
        {
            return _object.ToStringForInialization();
        }
    }
}

