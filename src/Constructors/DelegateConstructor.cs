using System;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class DelegateConstructor : IConstructor
    {
        private readonly Delegate _object;

        public DelegateConstructor(object @object)
            : this((Delegate)@object)
        {
        }
        
        public DelegateConstructor(Delegate @object)
        {
            _object = @object;
        }

        public bool IsValid => true;
        
        public IList<IArgument> Argumetns => new List<IArgument>();

        public override string ToString()
        {
            var actionMethod = _object.GetInvocationList()[0].Method;
            var parameters = actionMethod.GetParameters();
            var names = string.Join(", ", parameters.Select(p => p.Name));
            if (parameters.Length == 1)
            {
                return $"{names} => {{ }}";
            }
            return $"({names}) => {{ }}";
        }
    }
}