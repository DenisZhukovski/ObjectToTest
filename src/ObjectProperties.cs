using System.Linq;
using System.Reflection;
using ObjectToTest.ConstructorParameters;

namespace ObjectToTest
{
    internal class ObjectProperties
    {
        private readonly object _object;
        private readonly IArguments _sharedArguments;

        public ObjectProperties(object @object, IArguments sharedArguments)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public override string ToString()
        {
            var properties = _object.GetType().GetProperties()
                .Where(x => x.CanWrite)
                .Select(p => $"{p.Name} = {PropertyValue(p)}");
            var propertiesStr = string.Join(", ", properties);
            if (string.IsNullOrWhiteSpace(propertiesStr))
            {
                return string.Empty;
            }

            return "{" + propertiesStr + "}";
        }

        private string PropertyValue(PropertyInfo property)
        {
            var propertyObject = property.GetValue(_object);
            var sharedArgument = _sharedArguments.Argument(propertyObject);
            if (sharedArgument != null)
            {
                return sharedArgument.ToString();
            }
            return propertyObject.ToStringForInialization();
        }
    }
}
