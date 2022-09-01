using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectToTest.ConstructorParameters
{
    internal class CollectionArgument : ObjectConstructorParameter
    {
        public CollectionArgument(object @object, ParameterInfo parameter, IArguments sharedArguments)
            : base(@object, parameter, sharedArguments)
        {
        }

        public override string ToString()
        {
            var value = _object.Value(_parameter);
            var stringBuilder = new StringBuilder();

            if(value is IEnumerable collection)
            {
                foreach(var item in collection)
                {
                    stringBuilder.Append(item.ToStringForInialization()).Append(", ");
                }

                var paramsStr = stringBuilder
                    .Remove(stringBuilder.Length - 2, 2)
                    .ToString();

                var type = value.GetType();

                if(type.BaseType == typeof(Array))
                {
                    return $"new[] {{ {paramsStr} }}";
                }

                return type.IsGenericType
                        ? $"new {type.GenericTypeName()} {{ {paramsStr} }}"
                        : $"new {type.Name} {{ {paramsStr} }}";
            }

            return $"{value}";
        }
    }
}
