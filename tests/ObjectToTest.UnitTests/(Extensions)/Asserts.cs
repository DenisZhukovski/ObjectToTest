using Newtonsoft.Json.Linq;
using ObjectToTest.UnitTests.Extensions;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public static class Asserts
    {
        public static void EqualJson(string expectedJson, string actualJson, ITestOutputHelper? output = null)
        {
            expectedJson = expectedJson.NoNewLines();
            actualJson = actualJson.NoNewLines();
            JObject expected = JObject.Parse(expectedJson);
            JObject actual = JObject.Parse(actualJson);
            if (output != null)
            {
                output.WriteLine("Expected:" + expectedJson);
                output.WriteLine("Actual:" + actualJson);
            }

            Xunit.Assert.Equal(expected, actual, JToken.EqualityComparer);
        }
    }
}
