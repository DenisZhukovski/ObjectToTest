using System;
using Xunit;
using Xunit.Abstractions;
using ObjectToTest.UnitTests.Models;
using ObjectToTest.UnitTests.Data;
using System.Collections.Generic;

namespace ObjectToTest.UnitTests
{
    public class ToTestTests
    {
        private readonly ITestOutputHelper _output;

        public ToTestTests(ITestOutputHelper output)
        {
            _output = output;
        }

#pragma warning disable CS8604
        [Fact]
        public void ThrowArgumentNullException()
        {
            object? obj = null;
            Assert.Throws<ArgumentNullException>(() => obj.ToTest());
        }
#pragma warning restore CS8604

        [Fact]
        public void DefaultConstructor()
        {
            Assert.Equal(
                "new EmptyObject()",
                new EmptyObject().ToTest().Log(_output)
            );
        }

        [Fact]
        /*
         * Its been decided not to initialize the properties that have default value.
         * In this particular case PropertyName is declared as nullable and null by default
         * but it should not be reflected during initialization.
         */
        public void NullPropertyInitializer()
        {
            Assert.Equal(
                "new WithOnePublicProperty()",
                new WithOnePublicProperty().ToTest().Log(_output)
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
                }.ToTest().Log(_output)
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
                }.ToTest().Log(_output)
            );
        }

        [Fact]
        public void MultipleValueTypesPropertyInitializer()
        {
            Assert.Equal(
                "new WithTwoProperties(){IntProperty = 42, StringProperty = \"Test\"}",
                 new WithTwoProperties
                 {
                     IntProperty = 42,
                     StringProperty = "Test"
                 }.ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithIntArgumentForProperty()
        {
            Assert.Equal(
                "new WithOneParameterContructorAndPublicReadProperty(42)",
                new WithOneParameterContructorAndPublicReadProperty(42)
                        .ToTest()
                        .Log(_output)
            );
        }

        [Fact]
        public void CtorWithIntArgumentForPrivateField()
        {
            Assert.Equal(
                "new WithOneParamAndPrivateField(42)",
                new WithOneParamAndPrivateField(42).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithMultipleValueTypeArguments()
        {
            Assert.Equal(
                "new WithTwoParamOneFieldAndOneProperty(42,\"Test\")",
                new WithTwoParamOneFieldAndOneProperty(
                    42,
                    "Test"
                ).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithReferenceTypeArgument()
        {
            Assert.Equal(
                "new WithClassParam(new EmptyObject())",
                new WithClassParam(new EmptyObject()).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithMultipleComplexTypesArguments()
        {
            Assert.Equal(
                "new WithClassAndIntParams(42,new EmptyObject())",
                new WithClassAndIntParams(
                    42,
                    new EmptyObject()
                ).ToTest().Log(_output)
            );
        }

        [Fact]
        public void TimeSpanConstructor()
        {
            Assert.Equal(
                "new TimeSpan(18,17,34,24,5)",
                new TimeSpan(18, 17, 34, 24, 5)
                        .ToTest()
                        .Log(_output)
            );
        }

        [Fact]
        public void CtorWithComplexDependencyArgument()
        {
            Assert.Equal(
                "new WithClassParamThatDependsOnClass(new WithClassParam(new EmptyObject()))",
                new WithClassParamThatDependsOnClass(
                    new WithClassParam(new EmptyObject())
                ).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithComplexDependencySeveralArguments()
        {
            /*
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
                ).ToTest().Log(_output)
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
                 ).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithGenericArgument()
        {
            Assert.Equal(
                "new WithGenericArgument<IPrice>()",
                new WithGenericArgument<IPrice>().ToTest().Log(_output)
            );
        }

        [Fact]
        public void With2GenericArguments()
        {
            Assert.Equal(
                "new With2GenericArguments<IPrice,IUser>(new Price(10))",
                new With2GenericArguments<IPrice, IUser>(new Price(10))
                        .ToTest()
                        .Log(_output)
            );
        }

        [Fact]
        public void With3GenericArguments()
        {
            Assert.Equal(
                "new With3GenericArguments<int,decimal,string>()",
                  new With3GenericArguments<int, decimal, string>()
                      .ToTest()
                      .Log(_output)
            );
        }

        [Fact]
        public void CtorWithStructArgument()
        {
            Assert.Equal(
               "new WithStructArgument(new Vector3(0,0,1))",
                new WithStructArgument(UnityEngine.Vector3.forward)
                     .ToTest()
                     .Log(_output)
            );
        }

        [Fact]
        public void CtorWithEnumArgument()
        {
            Assert.Equal(
               "new WithEnumArgument(FlaggedEnum.Advanced)",
                new WithEnumArgument(FlaggedEnum.Advanced)
                     .ToTest()
                     .Log(_output)
            );
        }

        [Fact]
        public void CtorWithFuncArgument()
        {
            Assert.Equal(
               "new WithFuncArgument(() => 0)",
                new WithFuncArgument(() => 0)
                     .ToTest()
                     .Log(_output)
           );
        }

        [Fact]
        public void OtherObjectMethodReferenceAsArgument()
        {
            Assert.Equal(
                "new WithFuncArgument(new User(\"user Name\").Age)",
                new WithFuncArgument(new User("user Name").Age)
                    .ToTest()
                    .Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed")]
        public void SharedOtherObjectMethodsReferenceAsArgument()
        {        
            /*
            * @todo #:60m/DEV Make SharedOtherObjectMethodsReferenceAsArgument test to be green.
            * Now DelegateConstructor does not support shared objects.
            * DelegateConstructor should be able to generate code with shared arguments in such cases.
            */
            var user = new User("user name");
            Assert.Equal(
                $"var user = new User(\"user Name\");{Environment.NewLine}" +
                $"new With2FuncArguments(user.Age, user.LoginToAsync)",
                new With2FuncArguments(user.Age, user.LoginToAsync)
                    .ToTest()
                    .Log(_output)
            );
        }

        [Fact]
        public void CtorWithActionArgument()
        {
            Assert.Equal(
               "new WithActionArgument(pos => {})",
                new WithActionArgument((pos) => { })
                     .ToTest()
                     .Log(_output)
           );
        }

        [Fact]
        public void CtorWithIEnumerableInt()
        {
            Assert.Equal(
                "new WithIEnumerableInt(new[] { 1, 2, 4, 5 })",
                new WithIEnumerableInt(new[] { 1, 2, 4, 5 })
                        .ToTest()
                        .Log(_output)
            );
        }

        [Fact]
        public void CtorWithListInt()
        {
            Assert.Equal(
                "new WithListArgument(new List<int> { 1, 2, 4, 5 })",
                new WithListArgument(new List<int> { 1, 2, 4, 5 })
                        .ToTest()
                        .Log(_output)
            );
        }

        [Fact]
        public void CtorWithEmptyListInt()
        {
            Assert.Equal(
               "new WithListArgument(new List<int>())",
               new WithListArgument(new List<int>())
                       .ToTest()
                       .Log(_output)
           );
        }

        [Fact]
        public void CtorWithDictionaryIntString()
        {
            Assert.Equal(
                "new WithDictionaryArgument(new Dictionary<int,string> { { 1, \"1\" }, { 2, \"2\" }, { 3, \"3\" } })",
                new WithDictionaryArgument(
                    new Dictionary<int, string> { { 1, "1" }, { 2, "2" }, { 3, "3" } }
                ).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithEmptyDictionaryIntString()
        {
            Assert.Equal(
                "new WithDictionaryArgument(new Dictionary<int,string>())",
                new WithDictionaryArgument(
                    new Dictionary<int, string>()
                ).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CtorWithInterfaceArguments()
        {
            Assert.Equal(
                "new Foo(new Price(10),new User(\"User Name\"))",
                new Foo(
                    new Price(10),
                    new User("User Name")
                ).ToTest().Log(_output)
            );
        }

        [Fact]
        public void IncorrectArgumentsClass()
        {
            Assert.Equal(
                "Can not find a constructor for IncorrectArgumentsClass object, valid constructor is not available." +
                $"{Environment.NewLine}ctor IncorrectArgumentsClass{Environment.NewLine}" +
                $"  int first - not found in object{Environment.NewLine}" +
                $"  int second - not found in object{Environment.NewLine}",
                new IncorrectArgumentsClass(1, 2).ToTest().Log(_output)
            );
        }

        [Fact]
        public void CircularReferenceDetection()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            o1.PropertyName = o2;
            o2.PropertyName1 = o1;
            Assert.Equal(
                $"var circularRefPublicProperty2 = new CircularRefPublicProperty2();{Environment.NewLine}" +
                $"var circularRefPublicProperty1 = new CircularRefPublicProperty1();{Environment.NewLine}" +
                $"circularRefPublicProperty2.PropertyName1 = circularRefPublicProperty1;{Environment.NewLine}" +
                $"circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;{Environment.NewLine}" +
                "// Target object stored in: 'circularRefPublicProperty1'",
                o1.ToTest().Log(_output)
            );
        }

        [Fact]
        public void ComplexCircularReferenceDetection()
        {
            var o1 = new CircularRefPublicProperty1();
            var o2 = new CircularRefPublicProperty2();
            var o3 = new CircularRefPublicProperty3();
            o1.PropertyName = o2;
            o2.PropertyName3 = o3;
            o3.PropertyName = o1;
            Assert.Equal(
                $"var circularRefPublicProperty2 = new CircularRefPublicProperty2();{Environment.NewLine}" +
                $"var circularRefPublicProperty3 = new CircularRefPublicProperty3();{Environment.NewLine}" +
                $"var circularRefPublicProperty1 = new CircularRefPublicProperty1();{Environment.NewLine}" +
                $"circularRefPublicProperty2.PropertyName3 = circularRefPublicProperty3;{Environment.NewLine}" +
                $"circularRefPublicProperty3.PropertyName = circularRefPublicProperty1;{Environment.NewLine}" +
                $"circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;{Environment.NewLine}" +
                "// Target object stored in: 'circularRefPublicProperty1'",
                o1.ToTest().Log(_output)
            );
        }

        [Fact]
        public void TheSameObjectDetection()
        {
            var user = new User("user name");
            var withUser = new WithUserArgument(
                user,
                new WithUserPublicProperty
                {
                    User = user
                }
            );
            Assert.Equal(
                "var user = new User(\"user name\");" + Environment.NewLine +
                "new WithUserArgument(user,new WithUserPublicProperty(){User = user})",
                withUser.ToTest().Log(_output)
            );
        }

        [Fact]
        public void SingletonAsArgument()
        {
            Assert.Equal(
                "new WithSingletonArgument(SingletonClass.Instance)",
                new WithSingletonArgument(SingletonClass.Instance)
                        .ToTest()
                        .Log(_output)
            );
        }

        [Fact]
        public void SingletonAsSharedArgument()
        {
            Assert.Equal(
                "new SharedSingletons(new WithSingletonAndOtherArgument(SingletonClass.Instance,new Price(10)),new WithSingletonArgument(SingletonClass.Instance))",
                new SharedSingletons(
                    new WithSingletonAndOtherArgument(SingletonClass.Instance, new Price(10)),
                    new WithSingletonArgument(SingletonClass.Instance)
                ).ToTest()
                 .Log(_output)
            );
        }

        [Fact]
        public void SingletonWithOtherArgument()
        {
            Assert.Equal(
                "new WithSingletonAndOtherArgument(SingletonClass.Instance,new Price(10))",
                new WithSingletonAndOtherArgument(SingletonClass.Instance, new Price(10))
                    .ToTest()
                    .Log(_output)
            );
        }

        [Fact(Skip = "Need to fix this test")]
        public void NotFullyRecreatedWarningComment()
        {
            /*
             * @todo #:60m/LEAD
             * Warning comment when object has an internal state that can not be initialized through the constructor.
             * It means that the state of an object has been changed after its been created and it's happened through the method or event
             */

            Assert.Equal(
                @"
    /*
     * Warning!!!
     * Current object was not fully recreated because of some internal object field was changed
     * after the object was created.
     */
    new ChangedStateObject(new Price(10),new WithUserPublicProperty(){User = new User(""Test Name"")})",
                new ChangedStateObject(
                    new Price(10),
                    new WithUserPublicProperty
                    {
                        User = new User("Test Name")
                    }
                ).ChangeState()
                 .ToTest()
                 .Log(_output)
            );
        }
    }
}
