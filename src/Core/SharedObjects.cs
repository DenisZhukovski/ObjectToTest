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
            
            return WithoutSingleDelegates(_sharedObjects);
        }

        private List<object> WithoutSingleDelegates(List<object> sharedObjects)
        {
            for (int i = sharedObjects.Count - 1; i >= 0; i--)
            {
                if (sharedObjects[i] is Delegate @delegate)
                {
                    if (sharedObjects.Count(item => IsDelegateOrTarget(item, @delegate)) < 2)
                    {
                        sharedObjects.RemoveAt(i);
                    }
                }
            }

            return sharedObjects;
        }

        private bool IsDelegateOrTarget(object item, Delegate @delegate)
        {
            if (item is Delegate itemAsMethod)
            {
                return itemAsMethod.Target.Equals(@delegate.Target);
            }
            return item.Equals(@delegate.Target);
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
                if (!value.IsDelegate())
                {
                    SharedObjectsRecursive(value, allReferencedObjects);  
                }
            }
        }

        private void TryAddToShared(object @object, List<object> allReferencedObjects)
        {
            // was already used in other object
            var item = @object;
            if (item is Delegate @delegate)
            {
                item = @delegate.Target;
                allReferencedObjects.Add(item);
            }
            
            if (allReferencedObjects.Contains(item)) 
            {
                if (!AlreadyShared(@object))
                {
                    _sharedObjects.Add(@object);
                }
            }
            else
            {
                allReferencedObjects.Add(item);
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
                || @object.IsSingleton()
                || @object.IsCollection()
                || @object.IsValueType()
                || AlreadyShared(@object);
        }
    }
}
