using System.Net.Http;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class ObjectsUsageGraphTests
    {
        [Fact(Skip = "Should be fixed as a part of #180 bug")]
        public void HttpClient()
        {
            /*
             * @todo #180 60m/DEV ObjectsUsageGraph should be empty for HttpClient. The test should be green.
             */
            Assert.Empty(
                new ObjectsUsageGraph(new HttpClient()).ToDictionary()
            );
        }
    }
}