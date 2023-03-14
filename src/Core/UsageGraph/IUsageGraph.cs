using System.Collections.Generic;

namespace ObjectToTest
{
    public interface IUsageGraph
    {
        object Target { get; }
        
        Dictionary<object, int> ToDictionary();
    }
}