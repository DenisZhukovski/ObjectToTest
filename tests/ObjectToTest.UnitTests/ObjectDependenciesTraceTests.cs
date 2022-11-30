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

        [Fact(Skip = "Need to fix this test")]
        public void ComplexObjectDependenciesTrace()
        {
            /*
             * @todo #64 60m/DEV ObjectDependenciesTrace does not support recursive analyzes for invalid constructor
             * arguments. The test should be fixed and recursive analyzes implemented.
            */
            Assert.Equal(
                $"ctor ComplexObjectWithInvalidArguments{Environment.NewLine}" +
                $"  IUser user - invalid{Environment.NewLine}" +
                $"    ctor InvalidDependencyUser user{Environment.NewLine}" +
                $"      IPrice price - valid{Environment.NewLine}" +
                $"      IRepository repository - invalid{Environment.NewLine}" +
                $"        ctor IncorrectArgumentsClass{Environment.NewLine}" +
                $"          int first - not found in object{Environment.NewLine}" +
                $"          int second - not found in object{Environment.NewLine}" +
                $"  IPrice price - valid{Environment.NewLine}",
                new  ObjectDependenciesTrace(
                        new ComplexObjectWithInvalidArguments(
                            new InvalidDependencyUser(new Price(20), new IncorrectArgumentsClass(1, 2)),
                            new Price(30)
                        )
                ).ToString()
                 .Log(_output)
            );
        }
    }
}
