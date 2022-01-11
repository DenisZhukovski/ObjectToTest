using System;
using System.Reflection;

namespace ObjectToTest
{
    public class ObjectAsConstructor
    {
        private readonly object _object;

        public ObjectAsConstructor(object @object)
        {
            _object = @object ?? throw new ArgumentNullException(nameof(@object));
        }

        public override string ToString()
        {
            return $"new {Constructor().DeclaringType.Name}()";
        }

        private ConstructorInfo Constructor()
        {
            return new InternalStateConstructor(_object).Constructor();
        }
    }
}
