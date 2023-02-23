using System;
using Xunit;
using Xunit.Abstractions;
using ObjectToTest.UnitTests.Models;
using ObjectToTest.UnitTests.Data;
using System.Collections.Generic;
using System.Net.Http;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

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
                new EmptyObject().ToTest(_output)
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
                new WithOnePublicProperty().ToTest(_output)
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
                }.ToTest(_output, false)
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
                }.ToTest(_output, false)
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
                 }.ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithIntArgumentForProperty()
        {
            Assert.Equal(
                "new WithOneParameterConstructorAndPublicReadProperty(42)",
                new WithOneParameterConstructorAndPublicReadProperty(42).ToTest(_output)
            );
        }

        [Fact]
        public void CtorWithIntArgumentForPrivateField()
        {
            Assert.Equal(
                "new WithOneParamAndPrivateField(42)",
                new WithOneParamAndPrivateField(42).ToTest(_output)
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
                ).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithReferenceTypeArgument()
        {
            Assert.Equal(
                "new WithClassParam(new EmptyObject())",
                new WithClassParam(new EmptyObject()).ToTest(_output, false)
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
                ).ToTest(_output, false)
            );
        }

        [Fact]
        public void TimeSpanConstructor()
        {
            Assert.Equal(
                "new TimeSpan(18,17,34,24,5)",
                new TimeSpan(18, 17, 34, 24, 5).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithComplexDependencyArgument()
        {
            Assert.Equal(
                "new WithClassParamThatDependsOnClass(new WithClassParam(new EmptyObject()))",
                new WithClassParamThatDependsOnClass(
                    new WithClassParam(new EmptyObject())
                ).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithComplexDependencySeveralArguments()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                    "new WithTwoClassParamAndIntParam(",
                    "    new WithClassParam(",
                    "        new EmptyObject()",
                    "    ),",
                    "    new WithClassAndIntParams(",
                    "        42,",
                    "        new EmptyObject()",
                    "    ),",
                    "    42",
                    ")"
                ).ToString(),
                new WithTwoClassParamAndIntParam(
                    new WithClassParam(new EmptyObject()),
                    new WithClassAndIntParams(
                        42,
                        new EmptyObject()
                    ),
                    42
                ).ToTest(_output, true)
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
                 ).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithGenericArgument()
        {
            Assert.Equal(
                "new WithGenericArgument<IPrice>()",
                new WithGenericArgument<IPrice>().ToTest(_output)
            );
        }

        [Fact]
        public void With2GenericArguments()
        {
            Assert.Equal(
                "new With2GenericArguments<IPrice,IUser>(new Price(10))",
                new With2GenericArguments<IPrice, IUser>(new Price(10)).ToTest(_output, false)
            );
        }

        [Fact]
        public void With3GenericArguments()
        {
            Assert.Equal(
                "new With3GenericArguments<int,decimal,string>()",
                  new With3GenericArguments<int, decimal, string>().ToTest(_output)
            );
        }

        [Fact]
        public void CtorWithStructArgument()
        {
            Assert.Equal(
               "new WithStructArgument(new Vector3(0,0,1))",
                new WithStructArgument(UnityEngine.Vector3.forward).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithEnumArgument()
        {
            Assert.Equal(
               "new WithEnumArgument(FlaggedEnum.Advanced)",
                new WithEnumArgument(FlaggedEnum.Advanced).ToTest(_output)
            );
        }

        [Fact]
        public void CtorWithFuncArgument()
        {
            Assert.Equal(
               "new WithFuncArgument(() => 0)",
                new WithFuncArgument(() => 0).ToTest(_output, false)
           );
        }

        [Fact]
        public void OtherObjectMethodReferenceAsArgument()
        {
            Assert.Equal(
                "new WithFuncArgument(new User(\"user Name\").Age)",
                new WithFuncArgument(
                    new User("user Name").Age
                ).ToTest(_output, false)
            );
        }

        [Fact]
        public void SharedOtherObjectMethodsReferenceAsArgument()
        {        
            var user = new User("user name");
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}" +
                $"new With2FuncArguments(user.Age,user.LoginToAsync)",
                new With2FuncArguments(user.Age, user.LoginToAsync).ToTest(_output, false)
            );
        }

        [Fact]
        public void SharedArgumentWhenSeveralTimesAsCtorArgument()
        {
            var user = new User("user name");
            Assert.Equal(
                $"var user = new User(\"user name\");{Environment.NewLine}" +
                $"new With2ObjectArguments(user,user)",
                new With2ObjectArguments(user, user).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithActionArgument()
        {
            Assert.Equal(
               "new WithActionArgument(pos => {})",
                new WithActionArgument((pos) => { }).ToTest(_output, false)
           );
        }

        [Fact]
        public void CtorWithIEnumerableInt()
        {
            Assert.Equal(
                "new WithIEnumerableInt(new[] { 1, 2, 4, 5 })",
                new WithIEnumerableInt(new[] { 1, 2, 4, 5 }).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithListInt()
        {
            Assert.Equal(
                "new WithListArgument(new List<int> { 1, 2, 4, 5 })",
                new WithListArgument(new List<int> { 1, 2, 4, 5 }).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithEmptyListInt()
        {
            Assert.Equal(
               "new WithListArgument(new List<int>())",
               new WithListArgument(new List<int>()).ToTest(_output, false)
           );
        }

        [Fact]
        public void CtorWithDictionaryIntString()
        {
            Assert.Equal(
                "new WithDictionaryArgument(new Dictionary<int,string> { { 1, \"1\" }, { 2, \"2\" }, { 3, \"3\" } })",
                new WithDictionaryArgument(
                    new Dictionary<int, string> { { 1, "1" }, { 2, "2" }, { 3, "3" } }
                ).ToTest(_output, false)
            );
        }

        [Fact]
        public void CtorWithEmptyDictionaryIntString()
        {
            Assert.Equal(
                "new WithDictionaryArgument(new Dictionary<int,string>())",
                new WithDictionaryArgument(
                    new Dictionary<int, string>()
                ).ToTest(_output, false)
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
                ).ToTest(_output, false)
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
                new IncorrectArgumentsClass(1, 2).ToTest(_output)
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
                new NewLineSeparatedString(
                    $"var circularRefPublicProperty1 = new CircularRefPublicProperty1();",
                    $"var circularRefPublicProperty2 = new CircularRefPublicProperty2();",
                    $"circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;",
                    $"circularRefPublicProperty2.PropertyName1 = circularRefPublicProperty1;",
                    "// Target object stored in: 'circularRefPublicProperty1'"
                ).ToString(),
                o1.ToTest(_output)
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
                new NewLineSeparatedString(
                    $"var circularRefPublicProperty1 = new CircularRefPublicProperty1();",
                    $"var circularRefPublicProperty2 = new CircularRefPublicProperty2();",
                    $"var circularRefPublicProperty3 = new CircularRefPublicProperty3();",
                    $"circularRefPublicProperty1.PropertyName = circularRefPublicProperty2;",
                    $"circularRefPublicProperty2.PropertyName3 = circularRefPublicProperty3;",
                    $"circularRefPublicProperty3.PropertyName = circularRefPublicProperty1;",
                    "// Target object stored in: 'circularRefPublicProperty1'"
                ).ToString(),
                o1.ToTest(_output)
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
                withUser.ToTest(_output, false)
            );
        }

        [Fact]
        /*
         * It was an issue:
         *      When constructor argument is shared object the name and return type is not correct.
         *   It's because of the custom object class name which does not fit the argument's name and type.
         */
        public void SharedObjectButCustomClass()
        {
            var customUser = new CustomUser();
            var withUser = new WithUserArgument(
                customUser,
                new WithUserPublicProperty
                {
                    User = customUser
                }
            );
            Assert.Equal(
                new NewLineSeparatedString(
                    "var customUser = new CustomUser();",
                    "new WithUserArgument(customUser,new WithUserPublicProperty(){User = customUser})"
                ).ToString(),
                withUser.ToTest(_output, false)
            );
        }

        /*
         * When shared object is used several times its children also considered as shared objects
         */
        [Fact]
        public void SharedObjectWithDependencyUsedMultipleTimes()
        {
            var customUserWithDependency = new CustomUserWithDependency(new User("user name"));
            var withUser = new WithUserArgument(
                customUserWithDependency,
                new WithUserPublicProperty
                {
                    User = customUserWithDependency
                }
            );
            Assert.Equal(
                new NewLineSeparatedString(
                    "var customUserWithDependency = new CustomUserWithDependency(new User(\"user name\"));",
                    "new WithUserArgument(customUserWithDependency,new WithUserPublicProperty(){User = customUserWithDependency})"
                ).ToString(),
                withUser.ToTest(_output, false)
            );
        }

        [Fact]
        public void SingletonAsArgument()
        {
            Assert.Equal(
                "new WithSingletonArgument(SingletonClass.Instance)",
                new WithSingletonArgument(SingletonClass.Instance).ToTest(_output)
            );
        }

        [Fact]
        public void SingletonAsSharedArgument()
        {
            Assert.Equal(
                "new SharedSingletons(new WithSingletonAndOtherArgument(SingletonClass.Instance,new Price(10)),new WithSingletonArgument(SingletonClass.Instance))",
                new SharedSingletons(
                    new WithSingletonAndOtherArgument(
                        SingletonClass.Instance,
                        new Price(10)
                    ),
                    new WithSingletonArgument(SingletonClass.Instance)
                ).ToTest(_output, false)
            );
        }

        [Fact]
        public void SingletonWithOtherArgument()
        {
            Assert.Equal(
                "new WithSingletonAndOtherArgument(SingletonClass.Instance,new Price(10))",
                new WithSingletonAndOtherArgument(
                        SingletonClass.Instance,
                        new Price(10)
                ).ToTest(_output, false)
            );
        }
        
        [Fact(Skip = "Should be fixed as a part of #180 bug")]
        public void HttpClientToTest()
        {
            /*
             * @todo #180 60m/DEV HttpClient should be recreated properly, The test should be green.
             */
            Assert.Equal(
                "new HttpClient()",
                new HttpClient().ToTest(_output, false)
            );
        }

        [Fact(Skip = "Need to fix this test")]
        public void NotFullyRecreatedWarningComment()
        {
            /*
             * @todo #:60m/LEAD Warning comment when object has an internal state that can not be initialized through the constructor.
             * It means that the state of an object has been changed after its been created and it's happened through the method or event.
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
                 .ToTest(_output)
            );
        }
    }
}
