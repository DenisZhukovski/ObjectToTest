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

        public string Type => _object.GetType().TypeName();

        public bool IsValid => true;
        
        public IList<IArgument> Arguments => new List<IArgument>();

        public object? Object => _object;

        public override bool Equals(object? obj)
        {
            return _object.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }
        
        public override string ToString()
        {
            return _object.AsCode();
        }
    }
}