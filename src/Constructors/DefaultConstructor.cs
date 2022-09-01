using System.Linq;

namespace ObjectToTest.Constructors
{
    internal class DefaultConstructor : IConstructor
    {
        private readonly object _object;

        public DefaultConstructor(object @object)
        {
            _object = @object;
        }

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
