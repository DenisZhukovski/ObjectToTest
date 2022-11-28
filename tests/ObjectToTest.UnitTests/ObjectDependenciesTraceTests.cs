using System;
using ObjectToTest.UnitTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class ObjectDependenciesTraceTests
    {
        private readonly ITestOutputHelper _output;

        public ObjectDependenciesTraceTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void IncorrectArgumentsClass()
        {
            Assert.Equal(
                $"ctor IncorrectArgumentsClass{Environment.NewLine}" +
                $"  int first - not found in object{Environment.NewLine}" +
                $"  int second - not found in object{Environment.NewLine}",
                new  ObjectDependenciesTrace(new IncorrectArgumentsClass(1, 2))
                    .ToString()
                    .Log(_output)
            );
        }
    }
}
