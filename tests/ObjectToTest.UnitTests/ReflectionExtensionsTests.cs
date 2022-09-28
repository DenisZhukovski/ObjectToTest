using System;
using System.Collections.Generic;
using ObjectToTest.UnitTests.Models;
using UnityEngine;
using Xunit;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace ObjectToTest.UnitTests
{
    public class ReflectionExtensionsTests
    {
        [Fact]
        public void WithIndexatorValues()
        {
            Assert.Equal(
                new List<object> { 99 },
                new WithIndexator(99).Values()
            );
        }

        [Fact]
        public void ConstructorsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((object)null).Constructors());
        }

        [Fact]
        public void NullForEmptyPropertyName()
        {
            Assert.Null(
                new WithIndexator(99).Property(string.Empty)
            );
        }

        [Fact]
        public void ArgumentExceptionWhenNoValueExists()
        {
            Assert.Throws<ArgumentException>(() => new WithIndexator(99).Value("NonExistingValue"));
        }

        [Fact]
        public void StructIsValueType()
        {
            Assert.True(Vector3.down.IsValueType());
        }

        [Fact]
        public void NullNotValueType()
        {

            Assert.False(((object)null).IsValueType());
        }

        [Fact]
        public void ObjectNotValueType()
        {
            Assert.False(new WithIndexator(99).IsValueType());
        }

        [Fact]
        public void EmptyValuesForNull()
        {
            Assert.Empty(((object)null).Values());
        }

        [Fact]
        public void InvalidOperationExceptionForIndexer()
        {
            Assert.Throws<InvalidOperationException>(() => Vector3.forward.Value("Item"));
        }
    }
}

#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.