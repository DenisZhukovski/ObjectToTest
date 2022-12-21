using System;
using System.Collections.Generic;

namespace ObjectToTest
{
    public class ObjectsUsageGraph
    {
        private readonly object _root;

        public ObjectsUsageGraph(object root)
        {
            _root = root;
        }

        public Dictionary<object, int> ToDictionary()
        {
            var objectUsageCount = new Dictionary<object, int>();
            CollectObjectsUsageRecursive(_root, objectUsageCount);
            return objectUsageCount;
        }
        
        private void CollectObjectsUsageRecursive(object @object, Dictionary<object, int> objectUsageCount)
        {
            if (!objectUsageCount.ContainsKey(@object))
            {
                var usageCount = 1;
                if (IncrementCounterparts(@object, objectUsageCount))
                {
                    usageCount++;
                }
                objectUsageCount.Add(@object, usageCount);
                if (!Skip(@object))
                {
                    if (@object.HasCircularReference())
                    {
                        objectUsageCount[@object]++;
                    }
                    foreach (var value in @object.Values(true))
                    {
                        if (value != null)
                        {
                            CollectObjectsUsageRecursive(value, objectUsageCount);
                        }
                    }
                }
            }
            else
            {
                objectUsageCount[@object]++;
            }
        }
        
        private bool IncrementCounterparts(object @object, Dictionary<object, int> objectUsageCount)
        {
            var wasIncremented = false;
            if (@object is Delegate @delegate)
            {
                var objectsToIncrement = new List<object>();
                var delegateTarget = @delegate.Target;
                foreach (var itemToCheck in objectUsageCount.Keys)
                {
                    var item = itemToCheck;
                    if (item is Delegate itemToCheckDelegate)
                    {
                        item = itemToCheckDelegate.Target;
                    }

                    if (item.Equals(delegateTarget))
                    {
                        objectsToIncrement.Add(itemToCheck);
                    }
                }

                foreach (var itemToIncrement in objectsToIncrement)
                {
                    wasIncremented = true;
                    objectUsageCount[itemToIncrement]++;
                }
            }

            return wasIncremented;
        }
        
        private bool Skip(object? @object)
        {
            return @object == null
                   || @object.IsPrimitive()
                   || @object.IsSingleton()
                   || @object.IsDelegate()
                   || @object.IsCollection()
                   || @object.IsValueType();
        }
    }
}