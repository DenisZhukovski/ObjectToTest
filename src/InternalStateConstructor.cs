using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectToTest
{
    public class InternalStateConstructor
    {
        private readonly object _object;

        public InternalStateConstructor(object @object)
        {
            _object = @object ?? throw new ArgumentNullException(nameof(@object));
        }

        public string Name => _object.GetType().DeclaringType.Name;

        public IList<object> Arguments { get; }

        public IList<object> Properties { get; }

        public ConstructorInfo Constructor()
        {
            // need to find a constuctor that fits internal fields
            var objectType = _object.GetType();
            var privateFields = objectType.GetFields(BindingFlags.NonPublic);
            if (privateFields.Any())
            {
                return null;
                //foreach (var constructor in objectType.GetConstructors())
                //{
                //    if ()
                //}

            }
            else
            {
                return new DefaultConstructor(_object).Constructor();
            }
        }
    }
}
