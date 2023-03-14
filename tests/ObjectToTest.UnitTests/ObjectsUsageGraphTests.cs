using System.Net.Http;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ObjectsUsageGraphTests
    {
        [Fact]
        public void HttpClientHasNoMultiUsedFields()
        {
            Assert.Empty(
                new ObjectsUsageGraph(new HttpClient())
                    .ObjectArgumentsOnly()
                    .MultiUsedOnly()
            );
        }
    }
}