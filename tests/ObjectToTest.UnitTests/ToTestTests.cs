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
                "var o1=new EmptyObject();",
                new EmptyObject().ToTest()
            );
        }

        [Fact]
        public void ForObjectWithPublicProperty_ShouldReturnWithPropertyInitializer()
        {
            Assert.Equal(
                "var o1=new WithOnePublicProperty();o1.PropertyName = null;",
                new WithOnePublicProperty().ToTest()
            );
        }

        [Fact]
        public void ForObjectWithPublicPropertyThatHasStringValue_ShouldReturnWithInitializedProperty()
        {
            Assert.Equal(
                "var o1=new WithOnePublicProperty();o1.PropertyName = \"Test\";",
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
                "var o1=new WithOnePublicIntProperty();o1.PropertyName = 42;",
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
                "var o1=new WithTwoProperties();o1.IntProperty = 42;o1.StringProperty = \"Test\";",
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
                "var o1=new WithOneParameterContructorAndPublicReadProperty(42);",
                new WithOneParameterContructorAndPublicReadProperty(42).ToTest()
            );
        }

        [Fact]
        public void ForConsturctorWithOneParamAndPrivateField_ShouldReturnWithContructorWithValue()
        {
            Assert.Equal(
                "var o1=new WithOneParamAndPrivateField(42);",
                new WithOneParamAndPrivateField(42).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithTwoParamsOneFieldAndOneProperty_ShouldReturnWithConstructorWithValue()
        {
            Assert.Equal(
                "var o1=new WithTwoParamOneFieldAndOneProperty(42,\"Test\");",
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
                "var o2=new EmptyObject();var o1=new WithClassParam(o2);",
                new WithClassParam(new EmptyObject()).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithTwoParamOfClassAndInt_ShouldReturnWithTwoParam()
        {
            Assert.Equal(
                "var o2=new EmptyObject();var o1=new WithClassAndIntParams(42,o2);",
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
                "var o1=new TimeSpan(18,17,34,24,5);",
                new TimeSpan(18, 17, 34, 24, 5).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithClassParamThatDependsOnOtherClass_ShouldReturnValidResult()
        {
            Assert.Equal(
                "var o3=new EmptyObject();var o2=new WithClassParam(o3);var o1=new WithClassParamThatDependsOnClass(o2);",
                new WithClassParamThatDependsOnClass(
                    new WithClassParam(new EmptyObject())
                ).ToTest()
            );
        }

        [Fact]
        public void ForCtorWithTwoClassParamAndIntParam_ShouldReturnValidResult()
        {
            Assert.Equal(
                "var o3=new EmptyObject();var o2=new WithClassParam(o3);" +
                "var o5=new EmptyObject();var o4=new WithClassAndIntParams(42,o5);" +
                "var o1=new WithTwoClassParamAndIntParam(o2,o4,42);",
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
                "var o2=new WithOnePublicProperty();o2.PropertyName = \"Test\";" +
                "var o1=new WithClassParamWithProp(o2,42);",
                 new WithClassParamWithProp(
                     new WithOnePublicProperty { PropertyName = "Test" },
                     42
                 ).ToTest()
            );
        }
    }
}
