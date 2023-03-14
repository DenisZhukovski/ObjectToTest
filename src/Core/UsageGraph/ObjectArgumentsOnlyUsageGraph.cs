using System.Collections.Generic;

namespace ObjectToTest
{
    public class ObjectArgumentsOnlyUsageGraph : IUsageGraph
    {
        private readonly IUsageGraph _origin;

        public ObjectArgumentsOnlyUsageGraph(IUsageGraph origin)
        {
            _origin = origin;
        }
        
        public object Target => _origin.Target;
        
        public Dictionary<object, int> ToDictionary()
        {
            var objectsUsageCount = _origin.ToDictionary();
            var objectArgumentsUsageCount = new Dictionary<object, int>();
            foreach (var value in objectsUsageCount.Keys)
            {
                if ((Target == value || Target.ContainsValue(value)) && !objectArgumentsUsageCount.ContainsKey(value))
                {
                    objectArgumentsUsageCount.Add(value, objectsUsageCount[value]);
                }
            }

            return objectArgumentsUsageCount;
        }
    }
}