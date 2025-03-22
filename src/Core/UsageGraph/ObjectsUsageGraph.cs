using System;
using System.Collections.Generic;

namespace ObjectToTest
{
    /// <summary>
    /// The main goal is to calculate how much times the objects been used in object tree.
    /// The code assumes that there are circular references of some objects been used several times in the graph.
    /// </summary>
    public class ObjectsUsageGraph : IUsageGraph
    {
        public ObjectsUsageGraph(object root)
        {
            Target = root;
        }

        public object Target { get; }

        public Dictionary<object, int> ToDictionary()
        {
            var recursiveObjectUsageCount = new Dictionary<object, int>();
            CollectObjectsUsageRecursive(Target, recursiveObjectUsageCount);
            return recursiveObjectUsageCount;
        }
        
        private void CollectObjectsUsageRecursive(object @object, Dictionary<object, int> objectUsageCount)
        {
            if (!objectUsageCount.ContainsKey(@object))
            {
                var usageCount = 1;
                if (IncrementDelegateCounterparts(@object, objectUsageCount))
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
                        if (value != null && !value.IsPrimitive() && !@object.Equals(value))
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
        
        private bool IncrementDelegateCounterparts(object @object, Dictionary<object, int> objectUsageCount)
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