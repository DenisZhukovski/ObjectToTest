using System;
using System.Collections.Generic;

namespace ObjectToTest
{
    /// <summary>
    /// The entity finds the objects that are part of current object internal state
    /// and at the same time they used in more than one object entity.
    /// https://github.com/DenisZhukovski/ObjectToTest/wiki/Shared-Objects-Detection
    /// </summary>
    public class SharedObjects
    {
        private readonly object? _object;
        private readonly IUsageGraph _usageGraph;
        private readonly List<object> _sharedObjects = new();

        public SharedObjects(object? @object, bool recursive)
            : this(
                @object,
                !recursive 
                    ? new ObjectsUsageGraph(@object).ObjectArgumentsOnly()
                    : new ObjectsUsageGraph(@object)
            )
        {
            _object = @object;
        }
        
        public SharedObjects(object? @object, IUsageGraph usageGraph)
        {
            _object = @object;
            _usageGraph = usageGraph;
        }
        
        public List<object> ToList()
        {
            _sharedObjects.Clear();
            if (_object != null)
            {
                var multiUsedObjects = _usageGraph.MultiUsedOnly();
                foreach (var objectAsKey in multiUsedObjects.Keys)
                {
                    if (!objectAsKey.IsSingleton())
                    {
                        _sharedObjects.Add(objectAsKey);
                        if (objectAsKey is Delegate @delegate && !_sharedObjects.Contains(@delegate.Target))
                        {
                            _sharedObjects.Add(@delegate.Target);
                        }
                    }
                }
            }

            return _sharedObjects;
        }
    }
}
