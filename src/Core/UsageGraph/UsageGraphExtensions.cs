using System.Collections.Generic;

namespace ObjectToTest
{
    public static class UsageGraphExtensions
    {
        public static Dictionary<object, int> MultiUsedOnly(this IUsageGraph usageGraph)
        {
            var objectUsageCount = usageGraph.ToDictionary();
            var multiUsed = new Dictionary<object, int>();
            foreach (var objectAsKey in objectUsageCount.Keys)
            {
                if (objectUsageCount[objectAsKey] > 1)
                {
                    multiUsed.Add(objectAsKey, objectUsageCount[objectAsKey]);
                }
            }

            return multiUsed;
        }

        public static IUsageGraph ObjectArgumentsOnly(this IUsageGraph usageGraph)
        {
            return new ObjectArgumentsOnlyUsageGraph(usageGraph);
        }
    }
}