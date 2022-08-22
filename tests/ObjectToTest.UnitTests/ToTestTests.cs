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
            var emptyObject = new EmptyObject();

            var result = emptyObject.ToTest();

            Assert.Equal(
                "var o1=new EmptyObject();",
                result
            );
        }

        [Fact]
        public void ForObjectWithPublicProperty_ShouldReturnWithPropertyInitializer()
        {
            var withOneProperty = new WithOnePublicProperty();

            var result = withOneProperty.ToTest();

            Assert.Equal(
                "var o1=new WithOnePublicProperty();o1.PropertyName = null;",
                result
            );
        }

        [Fact]
        public void ForObjectWithPublicPropertyThatHasStringValue_ShouldReturnWithInitializedProperty()
        {
            var withOneProperty = new WithOnePublicProperty();
            withOneProperty.PropertyName = "Test";

            var result = withOneProperty.ToTest();

            Assert.Equal(
                "var o1=new WithOnePublicProperty();o1.PropertyName = \"Test\";",
                result
            );
        }

        [Fact]
        public void ForObjectWithPublicPropertyThatHasIntValue_ShouldReturnWithInitializedProperty()
        {
            var withOneProperty = new WithOnePublicIntProperty();
            withOneProperty.PropertyName = 42;

            var result = withOneProperty.ToTest();

            Assert.Equal(
                "var o1=new WithOnePublicIntProperty();o1.PropertyName = 42;",
                result
            );
        }

        [Fact]
        public void ForObjectWithTwoPublicPropertiesWithDifferentTypes_ShouldReturnedWithInitializedProperty()
        {
            var withTwoProperties = new WithTwoProperties
            {
                IntProperty = 42,
                StringProperty = "Test"
            };

            var result = withTwoProperties.ToTest();

            Assert.Equal(
                "var o1=new WithTwoProperties();o1.IntProperty = 42;o1.StringProperty = \"Test\";",
                result
            );
        }

        [Fact]
        public void ForContructorWithOneParameterAndReadProperty_ShouldReturnWithContructorWithValue()
        {
            var withContructor = new WithOneParameterContructorAndPublicReadProperty(42);

            var result = withContructor.ToTest();

            Assert.Equal(
                "var o1=new WithOneParameterContructorAndPublicReadProperty(42);",
                result
            );
        }

        [Fact]
        public void ForConsturctorWithOneParamAndPrivateField_ShouldReturnWithContructorWithValue()
        {
            var withField = new WithOneParamAndPrivateField(42);

            var result = withField.ToTest();

            Assert.Equal(
                "var o1=new WithOneParamAndPrivateField(42);",
                result
            );
        }

        [Fact]
        public void ForCtorWithTwoParamsOneFieldAndOneProperty_ShouldReturnWithConstructorWithValue()
        {
            var withFieldAndProperty = new WithTwoParamOneFieldAndOneProperty(42, "Test");

            var result = withFieldAndProperty.ToTest();

            Assert.Equal(
                "var o1=new WithTwoParamOneFieldAndOneProperty(42,\"Test\");",
                result
            );
        }

        [Fact]
        public void ForCtorWithParameterOfClass_ShouldReturnWithCtorWithParameterOfClass()
        {
            var withParamClass = new WithClassParam(new EmptyObject());

            var result = withParamClass.ToTest();

            Assert.Equal(
                "var o2=new EmptyObject();var o1=new WithClassParam(o2);",
                result
            );
        }

        [Fact]
        public void ForCtorWithTwoParamOfClassAndInt_ShouldReturnWithTwoParam()
        {
            var withTwoParams = new WithClassAndIntParams(42, new EmptyObject());

            var result = withTwoParams.ToTest();

            Assert.Equal(
                "var o2=new EmptyObject();var o1=new WithClassAndIntParams(42,o2);",
                result
            );
        }

        [Fact]
        public void ForTimeSpan_ShouldReturnValidTimeSpan()
        {
            var timeSpan = new TimeSpan(18, 17, 34, 24, 5);

            var result = timeSpan.ToTest();

            Assert.Equal(
                "var o1=new TimeSpan(18,17,34,24,5);",
                result
            );
        }

        [Fact]
        public void ForCtorWithClassParamThatDependsOnOtherClass_ShouldReturnValidResult()
        {
            var withClassThatDependsOnClass = new WithClassParamThatDependsOnClass(new WithClassParam(new EmptyObject()));

            var result = withClassThatDependsOnClass.ToTest();

            Assert.Equal(
                "var o3=new EmptyObject();var o2=new WithClassParam(o3);var o1=new WithClassParamThatDependsOnClass(o2);",
                result
            );
        }

        [Fact]
        public void ForCtorWithTwoClassParamAndIntParam_ShouldReturnValidResult()
        {
            var result = new WithTwoClassParamAndIntParam(
                new WithClassParam(new EmptyObject()),
                new WithClassAndIntParams(
                    42,
                    new EmptyObject()
                ),
                42
            ).ToTest();

            Assert.Equal(
                "var o3=new EmptyObject();var o2=new WithClassParam(o3);" +
                "var o5=new EmptyObject();var o4=new WithClassAndIntParams(42,o5);" +
                "var o1=new WithTwoClassParamAndIntParam(o2,o4,42);",
                result
            );
        }

        [Fact]
        public void ForCtorWithClassParamWithPropAndIntField()
        {
            var withProp = new WithOnePublicProperty();
            withProp.PropertyName = "Test";
            var withClassParam = new WithClassParamWithProp(withProp, 42);

            var result = withClassParam.ToTest();

            Assert.Equal(
                "var o2=new WithOnePublicProperty();o2.PropertyName = \"Test\";" +
                "var o1=new WithClassParamWithProp(o2,42);",
                result
            );
        }
    }
}
