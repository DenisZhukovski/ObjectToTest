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
                SharedObjectsRecursive(_object, new Dictionary<object, List<object>>());
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

        private void SharedObjectsRecursive(object @object, Dictionary<object, List<object>> allReferencedObjects)
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
                    @object,
                    allReferencedObjects
                );
                objectStates.Add(value);
                if (!value.IsDelegate())
                {
                    SharedObjectsRecursive(value, allReferencedObjects);  
                }
            }
        }

        private void TryAddToShared(
            object @object,
            object parent,
            Dictionary<object, List<object>> allReferencedObjects)
        {
            if (!allReferencedObjects.ContainsKey(parent))
            {
                allReferencedObjects.Add(parent, new List<object>());
            }

            // was already used in other object
            var item = @object;
            if (item is Delegate @delegate)
            {
                item = @delegate.Target;

                // Here there is a trick. The code has to simulate multiple usage for delegates
                // just to be able to detect that multiple delegates were used
                allReferencedObjects[parent].Add(item);
                allReferencedObjects[parent].Add(item);
            }
            else if (!allReferencedObjects[parent].Contains(item))
            {
                allReferencedObjects[parent].Add(item);
            }

            if (MultipleUsage(item, allReferencedObjects) && !AlreadyShared(@object)) 
            {
                _sharedObjects.Add(@object);
            }
        }

        private bool MultipleUsage(
            object @object,
            Dictionary<object, List<object>> allReferencedObjects)
        {
            var count = 0;
            foreach (var pair in allReferencedObjects)
            {
                count += pair.Value.Count(item => item.Equals(@object));
                if (pair.Key == @object)
                {
                    count++;
                }
            }
            return count > 1;
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
