using System.Collections.Generic;

namespace ObjectToTest
{
    /// <summary>
    /// @todo #122 60m/DEV SharedObjects summary is a little bit difficult for quick understanding. Need to rephrase it
    /// 
    /// The entity finds the objects that are part of current object internal state
    /// but also used in more than one object entity.
    /// https://github.com/DenisZhukovski/ObjectToTest/wiki/Shared-Objects-Detection
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
                var objectUsageCount = new ObjectsUsageGraph(_object).ToDictionary();
                foreach (var objectAsKey in objectUsageCount.Keys)
                {
                    if (objectUsageCount[objectAsKey] > 1 && !objectAsKey.IsSingleton() && !objectAsKey.IsPrimitive())
                    {
                        _sharedObjects.Add(objectAsKey);
                    }
                }
            }

            return _sharedObjects;
        }
    }
}
