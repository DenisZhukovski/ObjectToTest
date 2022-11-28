using System;
using System.Collections.Generic;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    /*
     * @todo #5 60m/DEV Implement proper formatting. Base puzzle with all rules for references.
     * The rules are following:
     * 1. if there is new operator in arguments, all arguments should be properly formatted.
     * 2. spaces between arguments are required: (0, 0, 1) instead of (0,0,1).
     * 3. Lambdas are always from new string.
     * 4. Indention is 4 spaces.
     * 5. Inner properties should be from separate lines.
     * 6. Lines bigger than 80 characters should be properly formatted.
     * 7. All arrays should be placed with proper formatting.
     * 8. Dictionary should be formatted with separate set of rules.
    */

    /*
    * @todo #5 60m/DEV Implement rule: if there is new operator in arguments, all arguments should be properly formatted.
    */

    /*
    * @todo #5 60m/DEV Implement rule: spaces between arguments are required: (0, 0, 1) instead of (0,0,1).
    */

    /*
    * @todo #5 60m/DEV Implement rule: Lambdas are always from new string.
    */

    /*
    * @todo #5 60m/DEV Implement rule: Indention is 4 spaces.
    */

    /*
    * @todo #5 60m/DEV Implement rule: Inner properties should be from separate lines.
    */

    /*
    * @todo #5 60m/DEV Implement rule: Lines bigger than 80 characters should be properly formatted.
    */

    /*
    * @todo #5 60m/DEV Implement rule: All arrays should be placed with proper formatting.
    */

    /*
    * @todo #5 60m/DEV Implement rule: Dictionary should be formatted with separate set of rules.
    */

    /*
   * @todo #5 60m/DEV In ToTestWellFormattedTests:
     * review tests, remove duplicates, rewrite to use new API.
     * review failed tests and define what rules should be implemented to fulfill.
   */

    /*
    * @todo #5 60m/DEV Implement a test to check rule: Lines bigger than 80 characters should be properly formatted..
    */

    public class ToTestWellFormattedTests
    {
        private readonly ITestOutputHelper _output;

        public ToTestWellFormattedTests(ITestOutputHelper output)
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
        
        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void StringPropertyInitializer()
        {
            Assert.Equal(
                "new WithOnePublicProperty()" +
                "{" +
                "    PropertyName = \"Test\"" +
                "}",
                new WithOnePublicProperty
                {
                    PropertyName = "Test"
                }.ToTest().Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void IntPropertyInitializer()
        {
            Assert.Equal(
                "new WithOnePublicIntProperty()" +
                "{" +
                "    PropertyName = 42" +
                "}",
                new WithOnePublicIntProperty
                {
                    PropertyName = 42
                }.ToTest().Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void MultipleValueTypesPropertyInitializer()
        {
            Assert.Equal(
                "new WithTwoProperties()" +
                "{" +
                "    IntProperty = 42," +
                "    StringProperty = \"Test\"" +
                "}",
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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5, rule 2")]
        public void CtorWithMultipleValueTypeArguments()
        {
            Assert.Equal(
                "new WithTwoParamOneFieldAndOneProperty(42, \"Test\")",
                new WithTwoParamOneFieldAndOneProperty(
                    42,
                    "Test"
                ).ToTest().Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithReferenceTypeArgument()
        {
            Assert.Equal(
                "new WithClassParam(" +
                "    new EmptyObject()" +
                ")",
                new WithClassParam(new EmptyObject()).ToTest().Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithMultipleComplexTypesArguments()
        {
            Assert.Equal(
                "new WithClassAndIntParams(" +
                "    42," +
                "    new EmptyObject()" +
                ")",
                new WithClassAndIntParams(
                    42,
                    new EmptyObject()
                ).ToTest().Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5, rule 2")]
        public void TimeSpanConstructor()
        {
            Assert.Equal(
                "new TimeSpan(18, 17, 34, 24, 5)",
                new TimeSpan(18, 17, 34, 24, 5)
                    .ToTest()
                    .Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithComplexDependencyArgument()
        {
            Assert.Equal(
                "new WithClassParamThatDependsOnClass(" +
                "    new WithClassParam(" +
                "        new EmptyObject()" +
                "    )" +
                ")",
                new WithClassParamThatDependsOnClass(
                    new WithClassParam(new EmptyObject())
                ).ToTest().Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithComplexDependencySeveralArguments()
        {
            Assert.Equal(
                "new WithTwoClassParamAndIntParam(" +
                "    new WithClassParam(" +
                "        new EmptyObject()" +
                "    )," +
                "    new WithClassAndIntParams(" +
                "        42," +
                "        new EmptyObject()" +
                "    )," +
                "    42" +
                ")",
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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithComplexArgumentsAndProperties()
        {
            Assert.Equal(
                "new WithClassParamWithProp(" +
                "    new WithOnePublicProperty()" +
                "    {" +
                "        PropertyName = \"Test\"" +
                "    }," +
                "    42" +
                ")",
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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void With2GenericArguments()
        {
            Assert.Equal(
                "new With2GenericArguments<IPrice,IUser>(" +
                "    new Price(" +
                "        10" +
                "    )" +
                ")",
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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5, rule 2")]
        public void CtorWithStructArgument()
        {
            Assert.Equal(
                "new WithStructArgument(" +
                "    new Vector3(0, 0, 1)" +
                ")",
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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithFuncArgument()
        {
            Assert.Equal(
                "new WithFuncArgument(" +
                "    () => 0" +
                ")",
                new WithFuncArgument(() => 0)
                    .ToTest()
                    .Log(_output)
            ); 
        }
        
        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithActionArgument()
        {
            Assert.Equal(
                "new WithActionArgument(" +
                "    pos => {}" +
                ")",
                new WithActionArgument((pos) => { })
                    .ToTest()
                    .Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithIEnumerableInt()
        {
            Assert.Equal(
                "new WithIEnumerableInt(" +
                "    new[] " +
                "    {" +
                "        1," +
                "        2," +
                "        4," +
                "        5" +
                "    }" +
                ")",
                new WithIEnumerableInt(new[] { 1, 2, 4, 5 })
                    .ToTest()
                    .Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithListInt()
        {
            Assert.Equal(
                "new WithListArgument(" +
                "    new List<int> " +
                "    {" +
                "        1," +
                "        2," +
                "        4," +
                "        5" +
                "    }" +
                ")",
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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithDictionaryIntString()
        {
            Assert.Equal(
                "new WithDictionaryArgument(" +
                "    new Dictionary<int,string>" +
                "    {" +
                "        { 1, \"1\" }," +
                "        { 2, \"2\" }," +
                "        { 3, \"3\" }" +
                "    }" +
                ")",
                new WithDictionaryArgument(
                    new Dictionary<int,string> { { 1, "1" }, { 2, "2" }, { 3, "3" } }
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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void CtorWithInterfaceArguments()
        {
            Assert.Equal(
                "new Foo(" +
                "    new Price(10)," +
                "    new User(\"User Name\")" +
                ")",
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
            /*
            * @todo #5:60m/DEV Adjust this test with formatting rules.
            */

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

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void SingletonAsSharedArgument()
        {
            Assert.Equal(
                "new SharedSingletons(" +
                "    new WithSingletonAndOtherArgument(" +
                "        SingletonClass.Instance," +
                "        new Price(10)" +
                "    )," +
                "    new WithSingletonArgument(SingletonClass.Instance)" +
                ")",
                new SharedSingletons(
                        new WithSingletonAndOtherArgument(SingletonClass.Instance, new Price(10)),
                        new WithSingletonArgument(SingletonClass.Instance)
                    ).ToTest()
                    .Log(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void SingletonWithOtherArgument()
        {
            Assert.Equal(
                "new WithSingletonAndOtherArgument(" +
                "    SingletonClass.Instance," +
                "    new Price(10)" +
                ")",
                new WithSingletonAndOtherArgument(SingletonClass.Instance, new Price(10))
                    .ToTest()
                    .Log(_output)
            );
        }
    }
}