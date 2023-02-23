using System;
using System.Net.Http;
using ObjectToTest.Arguments;
using ObjectToTest.UnitTests.Extensions;
using ObjectToTest.UnitTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class SharedCircularPropertiesTests
    {
        private readonly ITestOutputHelper _output;

        public SharedCircularPropertiesTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void NotEmptyForSharedObject()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.NotEmpty(new SharedCircularProperties(new ObjectSharedArguments(o1)).ToList());
        }
        
        [Fact]
        public void Initialization()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.Equal(
                new NewLineSeparatedString(
                    "var circularRefPublicProperty1 = new CircularRefPublicProperty1();",
                    "var circularRefPublicProperty2 = new CircularRefPublicProperty2();",
                    "circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;",
                    "circularRefPublicProperty2.PropertyName1 = circularRefPublicProperty1;",
                    string.Empty
                ).ToString(),
                new SharedCircularProperties(
                    new ObjectSharedArguments(o1)
                ).ToString().Log(_output)
            );
        }
        
        [Fact(Skip = "Should be fixed as a part of #180 bug")]
        public void HttpClient()
        {
            /*
             * @todo #180 60m/DEV SharedCircularProperties should be empty for HttpClient. The test should be green.
             */
            Assert.Empty(
                new SharedCircularProperties(
                    new ObjectSharedArguments(new HttpClient())
                ).ToList()
            );
        }
    }
}