using System;
using ObjectToTest.Arguments;
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
                $"var circularRefPublicProperty2 = new CircularRefPublicProperty2();{Environment.NewLine}" +
                $"var circularRefPublicProperty1 = new CircularRefPublicProperty1();{Environment.NewLine}" +
                $"circularRefPublicProperty2.PropertyName1 = circularRefPublicProperty1;{Environment.NewLine}" +
                $"circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;{Environment.NewLine}",
                new SharedCircularProperties(
                    new ObjectSharedArguments(o1)
                ).ToString().Log(_output)
            );
        }
        
    }
}