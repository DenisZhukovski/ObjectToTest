using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class SingletonConstructor : IConstructor
    {
        private readonly object _object;
        private List<IArgument>? _arguments;

        public SingletonConstructor(object @object)
        {
            _object = @object;
        }
        
        public bool IsValid => true;
        public IList<IArgument> Argumetns => _arguments ??= new List<IArgument>();

        public override string ToString()
        {
            return $"{_object.GetType().Name}.{SingletonInstance().Name}";
        }

        private PropertyInfo SingletonInstance()
        {
            return _object.GetType()
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .First(p => p.PropertyType == _object.GetType() && p.CanRead);
        }
    }
}