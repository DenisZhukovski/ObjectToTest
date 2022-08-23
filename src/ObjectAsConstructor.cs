using System;
using System.Collections.Generic;
using System.Linq;
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
            var constructor = new InternalStateConstructor(_object);
            return $"new {constructor.Name}({constructor.Arguments.Join()}){constructor.Properties.Join()}";
        }
    }
}
