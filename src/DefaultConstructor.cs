using System;
using System.Linq;
using System.Reflection;

namespace ObjectToTest
{
    public class DefaultConstructor
    {
        private readonly object _object;

        public DefaultConstructor(object @object)
        {
            _object = @object ?? throw new ArgumentNullException(nameof(@object));
        }

        public ConstructorInfo Constructor()
        {
            return _object
                .GetType()
                .GetConstructors()
                .First(c => !c.GetParameters().Any());
        }
    }
}
