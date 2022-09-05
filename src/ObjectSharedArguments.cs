using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using ObjectToTest.Arguments;

namespace ObjectToTest
{
    /// <summary>
    /// The idea is to detect the set of objects that used more that one time in different target object arguments.
    /// It should allow to reuse the argument injecting it into other object's constructors or property setters.
    /// </summary>
    public class ObjectSharedArguments : IArguments
    {
        private readonly object _object;
        private List<IArgument>? _sharedObjects;

        public ObjectSharedArguments(object @object)
        {
            _object = @object;
        }

        private List<IArgument> SharedObjects
        {
            get
            {
                /**
                * @todo #12:60m/DEV Make a code refactoring for this property.
                * The property is too big and it looks like Loops for Fields and Properties are pretty the same.
                */
                if (_sharedObjects == null)
                {
                    _sharedObjects = new List<IArgument>();
                    List<object> internalObjects = new List<object>();
                    foreach (FieldInfo field in _object.GetType().GetRuntimeFields())
                    {
                        var fieldObject = _object.Value(field);
                        if (fieldObject == null || fieldObject.IsPrimitive())
                        {
                            continue;
                        }
                        if (!internalObjects.Contains(fieldObject))
                        {
                            _sharedObjects.Add(
                                new SharedArgument(
                                    new Argument(
                                        field.Name,
                                        fieldObject.ValidConstructor(this)
                                    )
                                )
                            );
                        }
                        else
                        {
                            internalObjects.Add(fieldObject);
                        }
                    }

                    foreach (PropertyInfo property in _object.GetType().GetProperties())
                    {
                        var propertyObject = _object.Value(property);
                        if (propertyObject == null || propertyObject.IsPrimitive())
                        {
                            continue;
                        }
                        if (!internalObjects.Contains(propertyObject))
                        {
                            _sharedObjects.Add(
                                new SharedArgument(
                                    new Argument(
                                        property.Name,
                                        propertyObject.ValidConstructor(this)
                                    )
                                )
                            );
                        }
                        else
                        {
                            internalObjects.Add(propertyObject);
                        }
                    }
                }
                return _sharedObjects;
            }
        }

        public IArgument? Argument(object argument)
        {
            return SharedObjects.FirstOrDefault(a => a.Equals(argument));
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}

