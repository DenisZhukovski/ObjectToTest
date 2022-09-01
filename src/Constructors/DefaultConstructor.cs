using System.Collections.Generic;
using System.Linq;
using ObjectToTest.ConstructorParameters;

namespace ObjectToTest.Constructors
{
    internal class DefaultConstructor : IConstructor
    {
        private readonly object _object;

        public DefaultConstructor(object @object)
        {
            _object = @object;
        }

        public bool IsValid => _object
                .GetType()
                .GetConstructors()
                .Any(c => !c.GetParameters().Any() && c.IsPublic);

        public IList<IArgument> Argumetns => new List<IArgument>();

        public override string ToString()
        {
            var objectType = _object.GetType();
            if (objectType.IsGenericType)
            {
                return $"new {objectType.GenericTypeName()}()";
            }
            return $"new {objectType.Name}()";
        }
    }
}
