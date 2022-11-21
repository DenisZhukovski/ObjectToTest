using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class CollectionConstructor : IConstructor
    {
        private readonly object _object;

        public CollectionConstructor(object @object)
        {
            _object = @object;
        }

        public bool IsValid => true;

        public IList<IArgument> Arguments => new List<IArgument>();

        public override bool Equals(object? obj)
        {
            return (obj is IConstructor constructor && constructor.Equals(_object)) || _object.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _object.GetHashCode();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            if (_object is IEnumerable collection)
            {
                var type = _object.GetType();

                if (typeof(IDictionary).IsAssignableFrom(type))
                {
                    var dictionary = (IDictionary)_object;

                    foreach(var key in dictionary.Keys)
                    {
                        stringBuilder.Append($"{{ {key.ToStringForInitialization()}, {dictionary[key].ToStringForInitialization()} }}").Append(", ");
                    }
                }
                else
                {
                    foreach (var item in collection)
                    {
                        stringBuilder.Append(item.ToStringForInitialization()).Append(", ");
                    }
                }

                var paramsStr = stringBuilder
                    .Remove(stringBuilder.Length - 2, 2)
                    .ToString();

                if (type.BaseType == typeof(Array))
                {
                    return $"new[] {{ {paramsStr} }}";
                }

                return type.IsGenericType
                        ? $"new {type.GenericTypeName()} {{ {paramsStr} }}"
                        : $"new {type.Name} {{ {paramsStr} }}";
            }

            return $"{_object}";
        }
    }
}

