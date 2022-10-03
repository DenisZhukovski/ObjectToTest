using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectToTest.Arguments
{
    public class SharedCircularProperties : IArguments
    {
        private readonly IArguments _origin;

        public SharedCircularProperties(IArguments origin)
        {
            _origin = origin;
        }

        public IArgument? Argument(object argument)
        {
            return _origin.Argument(argument);
        }

        public List<IArgument> ToList()
        {
            return _origin.ToList();
        }

        public override string ToString()
        {
            var baseInit = _origin.ToString();
            return $"{baseInit}{WithCircularProperties()}";
        }

        /// <summary>
        /// Its some kind of delayed initialization just to avoid infinite
        /// circular initialization process the shared objects just created without
        /// circular reference properties and after that the objects
        /// </summary>
        private string WithCircularProperties()
        {
            var arguments = new StringBuilder();
            foreach (var argument in _origin.ToList().Where(a => a.Object?.HasCircularReference() ?? false))
            {
                var circularProperties = argument.Object
                    ?.GetType()
                    .GetProperties()
                    .Where(propertyInfo => IsCircularProperty(argument.Object, propertyInfo)) ?? new List<PropertyInfo>();
                foreach (var circularProperty in circularProperties)
                {
                    arguments.AppendLine($"{argument.Name}.{circularProperty.Name} = {PropertyValue(circularProperty.GetValue(argument.Object))};");
                }
            }
            return arguments.ToString();
        }
        
        private bool IsCircularProperty(object @object, PropertyInfo property)
        {
            return property.CanWrite
                   && !property.GetIndexParameters().Any()
                   && !property.IsDefaultValue(property.GetValue(@object))
                   && property.GetValue(@object).HasCircularReference();
        }
        
        private string PropertyValue(object propertyObject)
        {
            var sharedArgument = this.Argument(propertyObject);
            if (sharedArgument != null)
            {
                return sharedArgument.ToString();
            }
            return propertyObject.ToStringForInialization();
        }
    }
}