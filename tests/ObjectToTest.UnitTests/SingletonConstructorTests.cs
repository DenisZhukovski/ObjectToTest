using System;
using System.Collections.Generic;
using ObjectToTest.Constructors;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SingletonConstructorTests
    {
        [Fact]
        public void Ctor()
        {
            Assert.Equal(
                "SingletonClass.Instance",
                new SingletonConstructor(SingletonClass.Instance).ToString()
            );
        }
        [Fact]
        public void NoArguments()
        {
            Assert.Empty(
                new SingletonConstructor(SingletonClass.Instance).Arguments
            );
        }

        [Fact]
        public void EqualAndHashCode()
        {
            Assert.Contains(
                new SingletonConstructor(SingletonClass.Instance),
                new List<IConstructor>
                {
                    new SingletonConstructor(SingletonClass.Instance)
                }
            );
        }
    }
}