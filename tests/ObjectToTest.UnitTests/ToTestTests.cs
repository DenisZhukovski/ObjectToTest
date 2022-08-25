using System;
using Xunit;
using Xunit.Abstractions;
using ObjectToTest.UnitTests.Models;
using ObjectToTest.UnitTests.Data;

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
        public void ThrowArgumentNullException()
        {
            object obj = null;

            Assert.Throws<ArgumentNullException>(() => obj.ToTest());
        }

        [Fact]
        public void DefaultConstructor()
        {
            Assert.Equal(
                "new EmptyObject()",
                new EmptyObject().ToTest()
            );
        }

        [Fact]
        public void NullPropertyInitializer()
        {
            Assert.Equal(
                "new WithOnePublicProperty(){PropertyName = null}",
                new WithOnePublicProperty().ToTest()
            );
        }

        [Fact]
        public void StringPropertyInitializer()
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
        public void IntPropertyInitializer()
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
        public void MultipleTypesPropertyInitializer()
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
        public void ContructorWithIntArgumentForProperty()
        {
            Assert.Equal(
                "new WithOneParameterContructorAndPublicReadProperty(42)",
                new WithOneParameterContructorAndPublicReadProperty(42).ToTest()
            );
        }

        [Fact]
        public void ContructorWithIntArgumentForPrivateField()
        {
            Assert.Equal(
                "new WithOneParamAndPrivateField(42)",
                new WithOneParamAndPrivateField(42).ToTest()
            );
        }

        [Fact]
        public void ContructorWithMultipleValueTypeArguments()
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
        public void CtorWithClassArgument()
        {
            Assert.Equal(
                "new WithClassParam(new EmptyObject())",
                new WithClassParam(new EmptyObject()).ToTest()
            );
        }

        [Fact]
        public void ContructorWithMultipleComplexTypesArguments()
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
        public void TimeSpanConstructor()
        {
            Assert.Equal(
                "new TimeSpan(18,17,34,24,5)",
                new TimeSpan(18, 17, 34, 24, 5).ToTest()
            );
        }

        [Fact]
        public void CtorWithComplexDependcyArgument()
        {
            Assert.Equal(
                "new WithClassParamThatDependsOnClass(new WithClassParam(new EmptyObject()))",
                new WithClassParamThatDependsOnClass(
                    new WithClassParam(new EmptyObject())
                ).ToTest()
            );
        }

        [Fact]
        public void CtorWithComplexDependcySeveralArguments()
        {
            /**
             * @todo #:60m/DEV Proper declaration format is expected. It would be nice to start each constructor argument on its own line with proper intend
             * new WithTwoClassParamAndIntParam(
             *  new WithClassParam(
             *      new EmptyObject()
             *  ),
             *  new WithClassAndIntParams(
             *      42,
             *      new EmptyObject()
             *  ),
             *  42
             * )
             */
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
        public void CtorWithComplexArgumentsAndProperties()
        {
            Assert.Equal(
                "new WithClassParamWithProp(new WithOnePublicProperty(){PropertyName = \"Test\"},42)",
                 new WithClassParamWithProp(
                     new WithOnePublicProperty { PropertyName = "Test" },
                     42
                 ).ToTest()
            );
        }

        [Fact(Skip = "Need to fix this test")]
        public void WithGenericArgument()
        {
            /**
             * @todo #:60m/DEV Make WithGenericArgument test to be green. Generic type constructors are not supported at the moment. Need to add the support.
             */
            Assert.Equal(
                "new WithGenericArgument<IPrice>()",
                new WithGenericArgument<IPrice>().ToTest()
            );
        }

        [Fact(Skip = "Need to fix this test")]
        public void WithIEnumerableInt()
        {
            /**
             * @todo #:60m/DEV Make WithIEnumerableInt test to be green. Collection argument type constructors are not supported at the moment. Need to add the support.
             */
            Assert.Equal(
                "new WithIEnumerableInt(new[] { 1, 2, 4, 5 })",
                new WithIEnumerableInt(new[] { 1, 2, 4, 5 }).ToTest()
            );
        }

        [Fact]
        public void WithInterfaceArgument()
        {
            Assert.Equal(
                "new Foo(new Price(10),new User(\"User Name\"))",
                new Foo(new Price(10), new User("User Name")).ToTest()
            );
        }

        [Fact(Skip = "Need to fix this test")]
        public void IncorrectArgumentsClass()
        {
            /**
             * @todo #:60m/DEV Make IncorrectArgumentsClass test to be green.
             */
            Assert.Equal(
                "Can not find a constructor for IncorrectArgumentsClass object, not valid constructor available",
                new IncorrectArgumentsClass(1, 2).ToTest()
            );
        }
        
    }
}
