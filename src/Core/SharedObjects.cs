using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest
{
    /// <summary>
    /// The entity finds the objects that are part of current object internal state
    /// but also used in more than one object entity.
    /// </summary>
    public class SharedObjects
    {
        private readonly object? _object;
        private readonly List<object> _sharedObjects = new();

        public SharedObjects(object? @object)
        {
            _object = @object;
        }

        public List<object> ToList()
        {
            _sharedObjects.Clear();
            if (_object != null)
            {
                SharedObjectsRecursive(_object, new List<object>());
            }
            
            return _sharedObjects;
        }

        private void SharedObjectsRecursive(object @object, List<object> allReferencedObjects)
        {
            var objectStates = new List<object>();
            foreach (var value in @object.Values())
            {
                if (value == null || Skip(value) || objectStates.Contains(value))
                {
                    continue;
                }

                TryAddToShared(
                    value,
                    allReferencedObjects
                );
                objectStates.Add(value);
                SharedObjectsRecursive(value, allReferencedObjects);
            }
        }

        private void TryAddToShared(object @object, List<object> allReferencedObjects)
        {
            // was already used in other object
            if (allReferencedObjects.Contains(@object)) 
            {
                if (!AlreadyShared(@object))
                {
                    _sharedObjects.Add(@object);
                }
            }
            else
            {
                allReferencedObjects.Add(@object);
            }
        }

        private bool AlreadyShared(object @object)
        {
            return _sharedObjects.Any(so => so.Equals(@object));
        }
        
        private bool Skip(object? @object)
        {
            return @object == null
                || @object.IsPrimitive()
                || @object.IsDelegate()
                || @object.IsSingleton()
                || @object.IsCollection()
                || @object.IsValueType()
                || AlreadyShared(@object);
        }
    }
}
