using System;
using Xunit;
using Xunit.Abstractions;
using ObjectToTest.UnitTests.Models;

namespace ObjectToTest.UnitTests
{
    public class ToTestTests
    {
        private readonly ITestOutputHelper _output;

        public ToTestTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ForNullObject_ShouldThrowArgumentNullException()
        {
            object obj = null;

            Assert.Throws<ArgumentNullException>(() => obj.ToTest());
        }

        [Fact]
        public void ForEmptyObject_ShouldGenerateDefaultConstructor()
        {
            Assert.Equal(
                "new EmptyObject()",
                new EmptyObject().ToTest()
            );
        }

        [Fact]
        public void ForObjectWithPublicProperty_ShouldReturnWithPropertyInitializer()
        {
            Assert.Equal(
                "new WithOnePublicProperty(){PropertyName = null}",
                new WithOnePublicProperty().ToTest()
            );
        }

        [Fact]
        public void ForObjectWithPublicPropertyThatHasStringValue_ShouldReturnWithInitializedProperty()
        {
            Assert.Equal(
                "new WithOnePublicProperty(){PropertyName = \"Test\"}",
                new WithOnePublicProperty
                {
                    PropertyName = "Test"
                }.ToTest()
            );
        }

        [Fact]
        public void ForObjectWithPublicPropertyThatHasIntValue_ShouldReturnWithInitializedProperty()
        {
            Assert.Equal(
                "new WithOnePublicIntProperty(){PropertyName = 42}",
                new WithOnePublicIntProperty
                {
                    PropertyName = 42
                }.ToTest()
            );
        }

        [Fact]
        public void ForObjectWithTwoPublicPropertiesWithDifferentTypes_ShouldReturnedWithInitializedProperty()
        {
            Assert.Equal(
                "new WithTwoProperties(){IntProperty = 42, StringProperty = \"Test\"}",
                 new WithTwoProperties
                 {
                     IntProperty = 42,
                     StringProperty = "Test"
                 }.ToTest()
            );
        }

        [Fact]
        public void ForContructorWithOneParameterAndReadProperty_ShouldReturnWithContructorWithValue()
        {
            Assert.Equal(
                "new WithOneParameterContructorAndPublicReadProperty(42)",
                new WithOneParameterContructorAndPublicReadProperty(42).ToTest()
            );
        }

        [Fact]
        public void ForConsturctorWithOneParamAndPrivateField_ShouldReturnWithContructorWithValue()
        {
            Assert.Equal(
                "new WithOneParamAndPrivateField(42)",
                new WithOneParamAndPrivateField(42).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithTwoParamsOneFieldAndOneProperty_ShouldReturnWithConstructorWithValue()
        {
            Assert.Equal(
                "new WithTwoParamOneFieldAndOneProperty(42,\"Test\")",
                new WithTwoParamOneFieldAndOneProperty(
                    42,
                    "Test"
                ).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithParameterOfClass_ShouldReturnWithCtorWithParameterOfClass()
        {
            Assert.Equal(
                "new WithClassParam(new EmptyObject())",
                new WithClassParam(new EmptyObject()).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithTwoParamOfClassAndInt_ShouldReturnWithTwoParam()
        {
            Assert.Equal(
                "new WithClassAndIntParams(42,new EmptyObject())",
                new WithClassAndIntParams(
                    42,
                    new EmptyObject()
                ).ToTest()
            );
        }

        [Fact]
        public void ForTimeSpan_ShouldReturnValidTimeSpan()
        {
            Assert.Equal(
                "new TimeSpan(18,17,34,24,5)",
                new TimeSpan(18, 17, 34, 24, 5).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithClassParamThatDependsOnOtherClass_ShouldReturnValidResult()
        {
            Assert.Equal(
                "new WithClassParamThatDependsOnClass(new WithClassParam(new EmptyObject()))",
                new WithClassParamThatDependsOnClass(
                    new WithClassParam(new EmptyObject())
                ).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithTwoClassParamAndIntParam_ShouldReturnValidResult()
        {
            Assert.Equal(
                "new WithTwoClassParamAndIntParam(new WithClassParam(new EmptyObject()),new WithClassAndIntParams(42,new EmptyObject()),42)",
                new WithTwoClassParamAndIntParam(
                    new WithClassParam(new EmptyObject()),
                    new WithClassAndIntParams(
                        42,
                        new EmptyObject()
                    ),
                    42
                ).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithClassParamWithPropAndIntField()
        {
            Assert.Equal(
                "new WithClassParamWithProp(new WithOnePublicProperty(){PropertyName = \"Test\"},42)",
                 new WithClassParamWithProp(
                     new WithOnePublicProperty { PropertyName = "Test" },
                     42
                 ).ToTest()
            );
        }
    }
}
