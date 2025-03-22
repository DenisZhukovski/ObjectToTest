using System;
using System.Collections.Generic;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Extensions;
using ObjectToTest.UnitTests.Models;
using Xunit;

namespace ObjectToTest.UnitTests
{
    /*
     * @todo #76 60m/DEV Add rules into documentation.
     * The rules are following:
     * 0. API should be compatible with old one.
     * 1. if there is new operator in arguments, all arguments should be from new lines.
     * 2. spaces between arguments are required if arguments list are in one line: (0, 0, 1) instead of (0,0,1).
     * 3. Lambdas are always from new string.
     * 4. Indention is 4 spaces.
     * 5. Inner properties should be from separate lines.
     * 6. Lines bigger than 80 characters should be properly formatted.
     * 7. All arrays should be placed with proper formatting. Each argument on separate line.
     * 8. Dictionary should be formatted with separate set of rules. Each pair on separate line.
    */

    public class ToTestWellFormattedTests(ITestOutputHelper output)
    {
#pragma warning disable CS8604
        [Fact]
        public void ThrowArgumentNullException_ShouldBehaveAsNotFormatted()
        {
            object? obj = null;
            Assert.Throws<ArgumentNullException>(() => obj.ToTestWellFormatted());
        }
#pragma warning restore CS8604
        
        [Fact]
        public void InnerPropertyShouldBeFromSeparateLine_StringPropertyInitializer()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                    "new WithOnePublicProperty()",
                    "{",
                    "    PropertyName = \"Test\"",
                    "}"
                ).ToString(),
                new WithOnePublicProperty
                {
                    PropertyName = "Test"
                }.ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void InnerPropertyShouldBeFromSeparateLine_IntPropertyInitializer()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithOnePublicIntProperty()",
                "{",
                "    PropertyName = 42",
                "}"
                ).ToString(),
                new WithOnePublicIntProperty
                {
                    PropertyName = 42
                }.ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void InnerPropertyShouldBeFromSeparateLine_MultipleValueTypesPropertyInitializer()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithTwoProperties()",
                "{",
                "    IntProperty = 42,",
                "    StringProperty = \"Test\"",
                "}"
                ).ToString(),
                new WithTwoProperties
                {
                    IntProperty = 42,
                    StringProperty = "Test"
                }.ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ShortArgumentsWithoutNewPlacedInline_CtorWithIntArgumentForProperty()
        {
            Assert.Equal(
                "new WithOneParameterConstructorAndPublicReadProperty(42)",
                new WithOneParameterConstructorAndPublicReadProperty(42)
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ShortArgumentsWithoutNewPlacedInline_CtorWithIntArgumentForPrivateField()
        {
            Assert.Equal(
                "new WithOneParamAndPrivateField(42)",
                new WithOneParamAndPrivateField(42)
                        .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ShortArgumentsWithoutNewPlacedInline_CtorWithMultipleValueTypeArguments()
        {
            Assert.Equal(
                "new WithTwoParamOneFieldAndOneProperty(42, \"Test\")",
                new WithTwoParamOneFieldAndOneProperty(
                    42,
                    "Test"
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void StringMoreThan80CharsShouldBeFormatted_CtorWithMultipleLongValueTypeArguments()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithTwoParamOneFieldAndOneProperty(",
                "    42,",
                "    \"Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test\"",
                ")"
                ).ToString(),
                new WithTwoParamOneFieldAndOneProperty(
                    42,
                    "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test"
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_CtorWithReferenceTypeArgument()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithClassParam(",
                "    new EmptyObject()",
                ")"
                ).ToString(),
                new WithClassParam(new EmptyObject())
                        .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_CtorWithMultipleComplexTypesArguments()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithClassAndIntParams(",
                "    42,",
                "    new EmptyObject()",
                ")"
                ).ToString(),
                new WithClassAndIntParams(
                    42,
                    new EmptyObject()
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ShortArgumentsShouldBeSeparatedBySpace_TimeSpanConstructor()
        {
            Assert.Equal(
                "new TimeSpan(18, 17, 34, 24, 5, 0)",
                new TimeSpan(18, 17, 34, 24, 5)
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_CtorWithComplexDependencyArgument()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithClassParamThatDependsOnClass(",
                "    new WithClassParam(",
                "        new EmptyObject()",
                "    )",
                ")"
                ).ToString(),
                new WithClassParamThatDependsOnClass(
                    new WithClassParam(new EmptyObject())
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_CtorWithComplexDependencySeveralArguments()
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
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void InnerPropertyShouldBeFromSeparateLine__And_ArgumentWithNewShouldBeFormatted__CtorWithComplexArgumentsAndProperties()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithClassParamWithProp(",
                "    new WithOnePublicProperty()",
                "    {",
                "        PropertyName = \"Test\"",
                "    },",
                "    42",
                ")"
                ).ToString(),
                new WithClassParamWithProp(
                    new WithOnePublicProperty { PropertyName = "Test" },
                    42
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void GenericArgumentsAreNotFormatted_CtorWithGenericArgument()
        {
            Assert.Equal(
                "new WithGenericArgument<IPrice>()",
                new WithGenericArgument<IPrice>()
                        .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void GenericArgumentsAreNotFormatted_With2GenericArguments()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new With2GenericArguments<IPrice,IUser>(",
                "    new Price(10)",
                ")"
                ).ToString(),
                new With2GenericArguments<IPrice, IUser>(new Price(10))
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void GenericArgumentsAreNotFormatted_With3GenericArguments()
        {
            Assert.Equal(
                "new With3GenericArguments<int,decimal,string>()",
                new With3GenericArguments<int, decimal, string>()
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ShortArgumentsShouldBeSeparatedBySpace_And_ArgumentWithNewShouldBeFormatted_CtorWithStructArgument()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithStructArgument(",
                "    new Vector3(0, 0, 1)",
                ")"
                ).ToString(),
                new WithStructArgument(UnityEngine.Vector3.forward)
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ShortArgumentsWithoutNewPlacedInline_CtorWithEnumArgument()
        {
            Assert.Equal(
                "new WithEnumArgument(FlaggedEnum.Advanced)",
                new WithEnumArgument(FlaggedEnum.Advanced)
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void LambdasShouldBePlacedOnNewLine_CtorWithFuncArgument()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithFuncArgument(",
                "    () => 0",
                ")"
                ).ToString(),
                new WithFuncArgument(() => 0)
                    .ToTestWellFormatted(output)
            ); 
        }
        
        [Fact]
        public void LambdasShouldBePlacedOnNewLine_CtorWithActionArgument()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithActionArgument(",
                "    pos => {}",
                ")"
                ).ToString(),
                new WithActionArgument((pos) => { })
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArraysShouldBeFormatted_CtorWithIEnumerableInt()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithIEnumerableInt(",
                "    new[]",
                "    {",
                "        1,",
                "        2,",
                "        4,",
                "        5",
                "    }",
                ")"
                ).ToString(),
                new WithIEnumerableInt(
                    new [] { 1, 2, 4, 5 }
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArraysShouldBeFormatted_CtorWithListInt()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithListArgument(",
                "    new List<int>()",
                "    {",
                "        1,",
                "        2,",
                "        4,",
                "        5",
                "    }",
                ")"
                ).ToString(),
                new WithListArgument(new List<int> { 1, 2, 4, 5 })
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_CtorWithEmptyListInt()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithListArgument(",
                "    new List<int>()",
                ")"
                ).ToString(),
                new WithListArgument(new List<int>())
                    .ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void DictionaryShouldBeFormatted_CtorWithDictionaryIntString()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithDictionaryArgument(",
                "    new Dictionary<int,string>()",
                "    {",
                "        { 1, \"1\" },",
                "        { 2, \"2\" },",
                "        { 3, \"3\" }",
                "    }",
                ")"
                ).ToString(),
                new WithDictionaryArgument(
                    new Dictionary<int,string> { { 1, "1" }, { 2, "2" }, { 3, "3" } }
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_CtorWithEmptyDictionaryIntString()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithDictionaryArgument(",
                "    new Dictionary<int,string>()",
                ")"
                ).ToString(),
                new WithDictionaryArgument(
                    new Dictionary<int, string>()
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_And_ShortArgumentsWithoutNewPlacedInline_CtorWithInterfaceArguments()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new Foo(",
                "    new Price(10),",
                "    new User(\"User Name\")",
                ")"
                ).ToString(),
                new Foo(
                    new Price(10),
                    new User("User Name")
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_And_InnerPropertyShouldBeFromSeparateLine_TheSameObjectDetection()
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
                new NewLineSeparatedString(
                "var user = new User(\"user name\");",
                "new WithUserArgument(",
                "    user,",
                "    new WithUserPublicProperty()",
                "    {",
                "        User = user",
                "    }",
                ")"
                ).ToString(),
                withUser.ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_SingletonAsSharedArgument()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new SharedSingletons(",
                "    new WithSingletonAndOtherArgument(",
                "        SingletonClass.Instance,",
                "        new Price(10)",
                "    ),",
                "    new WithSingletonArgument(SingletonClass.Instance)",
                ")"
                ).ToString(),
                new SharedSingletons(
                    new WithSingletonAndOtherArgument(SingletonClass.Instance, new Price(10)),
                    new WithSingletonArgument(SingletonClass.Instance)
                ).ToTestWellFormatted(output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldBeFormatted_SingletonWithOtherArgument()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                    "new WithSingletonAndOtherArgument(",
                    "    SingletonClass.Instance,",
                    "    new Price(10)",
                    ")"
                ).ToString(),
                new WithSingletonAndOtherArgument(SingletonClass.Instance, new Price(10))
                    .ToTestWellFormatted(output)
            );
        }
        
        [Fact]
        public void IncorrectArgumentsClass()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "Can not find a constructor for IncorrectArgumentsClass object, valid constructor is not available.",
                    "ctor IncorrectArgumentsClass",
                    "  int first - not found in object",
                    "  int second - not found in object",
                    string.Empty
                ).ToString(),
                new IncorrectArgumentsClass(1, 2)
                    .ToTestWellFormatted(output)
            );
        }
    }
}