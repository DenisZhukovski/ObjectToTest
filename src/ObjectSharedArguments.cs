using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
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
                if (_sharedObjects == null)
                {
                    _sharedObjects = new List<IArgument>();
                    SharedObjectsRecursive(_object, new List<object>());
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
            var arguments = new StringBuilder();
            foreach (var argument in SharedObjects)
            {
                arguments.AppendLine(argument.ToString() + ";");
            }
            return arguments.ToString();
        }

        /**
        * @todo #12:60m/DEV Make a code refactoring for this method.
        * The method is too big and complex. It looks like the Loops for Fields and Properties are pretty the same.
        */
        private void SharedObjectsRecursive(object @object, List<object> internalObjects)
        {
            var currentObjectStates = new List<object>();
            foreach (FieldInfo field in @object.GetType().GetRuntimeFields())
            {
                var fieldObject = @object.Value(field);
                if (Skip(fieldObject) || currentObjectStates.Contains(fieldObject))
                {
                    continue;
                }
               
                if (internalObjects.Contains(fieldObject))
                {
                    if (!_sharedObjects.Any(so => so.Equals(fieldObject)))
                    {
                        _sharedObjects.Add(
                            new SharedArgument(
                                new Argument(
                                    VariableName(fieldObject),
                                    fieldObject.ValidConstructor(this)
                                )
                            )
                        );
                    }
                }
                else
                {
                    internalObjects.Add(fieldObject);
                }
                currentObjectStates.Add(fieldObject);
                SharedObjectsRecursive(fieldObject, internalObjects);
            }

            foreach (PropertyInfo property in @object.GetType().GetProperties())
            {
                var propertyObject = @object.Value(property);
                if (Skip(propertyObject) || currentObjectStates.Contains(propertyObject))
                {
                    continue;
                }
                if (internalObjects.Contains(propertyObject))
                {
                    if (!_sharedObjects.Any(so => so.Equals(propertyObject)))
                    {
                        _sharedObjects.Add(
                            new SharedArgument(
                                new Argument(
                                    VariableName(propertyObject),
                                    propertyObject.ValidConstructor(this)
                                )
                            )
                        );
                    }
                }
                else
                {
                    internalObjects.Add(propertyObject);
                }
                currentObjectStates.Add(propertyObject);
                SharedObjectsRecursive(propertyObject, internalObjects);
            }
        }

        private string VariableName(object @object)
        {
            return Char.ToLower(@object.GetType().Name[0]) + @object.GetType().Name.Substring(1);
        }

        private bool Skip(object? @object)
        {
            return @object == null || @object.IsPrimitive() || @object.IsCollection() || @object is TimeSpan || @object is DateTime;
        }
    }
}

