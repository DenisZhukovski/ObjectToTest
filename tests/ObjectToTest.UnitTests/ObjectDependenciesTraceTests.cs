using System;
using ObjectToTest.UnitTests.Extensions;
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
                new NewLineSeparatedString(
                    "ctor IncorrectArgumentsClass",
                    "  int first - not found in object",
                    "  int second - not found in object",
                    string.Empty
                ).ToString(),
                new  ObjectDependenciesTrace(new IncorrectArgumentsClass(1, 2))
                    .ToString()
                    .Log(_output)
            );
        }

        [Fact]
        public void InvalidDependencyUser()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                    "ctor InvalidDependencyUser",
                    "  IPrice price - valid",
                    "  IRepository repository - invalid",
                    "    ctor IncorrectArgumentsClass",
                    "      int first - not found in object",
                    "      int second - not found in object",
                    string.Empty
                ).ToString(),
                new  ObjectDependenciesTrace(
                        new InvalidDependencyUser(
                            new Price(20),
                            new IncorrectArgumentsClass(1, 2)
                        )
                ).ToString()
                 .Log(_output)
            );
        }

        [Fact]
        public void ComplexObjectDependenciesTrace()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                    "ctor ComplexObjectWithInvalidArguments",
                    "  IUser user - invalid",
                    "    ctor InvalidDependencyUser",
                    "      IPrice price - valid",
                    "      IRepository repository - invalid",
                    "        ctor IncorrectArgumentsClass",
                    "          int first - not found in object",
                    "          int second - not found in object",
                    "  IPrice price - valid",
                    string.Empty
                ).ToString(),
                new ObjectDependenciesTrace(
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
