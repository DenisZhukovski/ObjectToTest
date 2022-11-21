using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectToTest.Arguments;

namespace ObjectToTest
{
    internal class ObjectProperties
    {
        private readonly object _object;
        private readonly IArguments _sharedArguments;
        private readonly Func<object, PropertyInfo, bool> _isValidProperty;

        public ObjectProperties(object @object, IArguments sharedArguments)
            : this(@object, sharedArguments, ShouldBeInitialized)
        {
        }
        
        public ObjectProperties(
            object @object, 
            IArguments sharedArguments, 
            Func<object, PropertyInfo, bool> isValidProperty)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
            _isValidProperty = isValidProperty;
        }

        public IList<PropertyInfo> ToList()
        {
            return _object
                .GetType()
                .GetProperties()
                .Where(p => _isValidProperty(_object, p))
                .ToList();
        }
        
        public override string ToString()
        {
            var properties = ToList().Select(
                p => $"{p.Name} = {PropertyValue(p)}"
            );
            var propertiesStr = string.Join(", ", properties);
            if (string.IsNullOrWhiteSpace(propertiesStr))
            {
                return string.Empty;
            }

            return "{" + propertiesStr + "}";
        }

        private static bool ShouldBeInitialized(object @object, PropertyInfo propertyInfo)
        {
            return propertyInfo.CanWrite
                   && !propertyInfo.GetIndexParameters().Any()
                   && !propertyInfo.IsDefaultValue(propertyInfo.GetValue(@object))
                   && !propertyInfo.GetValue(@object).HasCircularReference();
        }
        
        private string PropertyValue(PropertyInfo property)
        {
            var propertyObject = property.GetValue(_object);
            var sharedArgument = _sharedArguments.Argument(propertyObject);
            if (sharedArgument != null)
            {
                return sharedArgument.ToString();
            }
            return propertyObject.ToStringForInitialization();
        }
    }
}
