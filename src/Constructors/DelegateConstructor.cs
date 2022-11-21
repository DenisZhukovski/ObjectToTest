using System;
using System.Collections.Generic;
using ObjectToTest.Arguments;
using ObjectToTest.Extensions;

namespace ObjectToTest.Constructors
{
    public class DelegateConstructor : IConstructor
    {
        private readonly Delegate _object;

        public DelegateConstructor(Action @object)
            : this((Delegate)@object)
        {
        }
        
        public DelegateConstructor(object @object)
            : this((Delegate)@object)
        {
        }
        
        public DelegateConstructor(Delegate @object)
        {
            _object = @object;
        }

        public bool IsValid => true;
        
        public IList<IArgument> Arguments => new List<IArgument>();

        public override string ToString()
        {
            return _object.AsCode();
        }
    }
}