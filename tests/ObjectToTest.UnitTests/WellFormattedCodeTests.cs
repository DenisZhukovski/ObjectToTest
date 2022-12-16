using System;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting;
using ObjectToTest.UnitTests.Data;
using ObjectToTest.UnitTests.Extensions;
using ObjectToTest.UnitTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class WellFormattedCodeTests
    {
        private readonly ITestOutputHelper _output;

        public WellFormattedCodeTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void NoNeedToFormatAlreadyFormattedCode()
        {
            Assert.Equal(
                "new Simple()",
                new WellFormattedCode("new Simple()").ToString(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void InnerPropertyShouldBeFromSeparateLine()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithProperty()",
                "{",
                "    IntProperty = 42,",
                "    PropertyName = \"Test\"",
                "}"
                ).ToString(),
                new WellFormattedCode(
                        "new WithProperty(){IntProperty = 42,PropertyName = \"Test\"}"
                ).ToString(_output)
            );
        }

        [Fact]
        public void NoNeedToFormatAlreadyFormattedCodeWithParam()
        {
            Assert.Equal(
                "new WithParam(42)",
                new WellFormattedCode("new WithParam(42)").ToString(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5, rule 2")]
        public void ArgumentsShoulBeSeparatedBySpace()
        {
            Assert.Equal(
                "new WithTwoParams(42, \"Test\")",
                new WellFormattedCode("new WithTwoParams(42,\"Test\")").ToString(_output)
            );
        }

        [Fact]
        public void StringMoreThan80CharsShouldBeFormatted()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithTwoParamOneFieldAndOneProperty(",
                "    42,",
                "    \"Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test\"",
                ")"
                ).ToString(),
                new WellFormattedCode(
                    "new WithTwoParamOneFieldAndOneProperty(42,\"Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test\")"
                ).ToString(_output)
            );
        }

        [Fact]
        public void ArgumentWithNewShouldStartFromNewLine()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithClassParam(",
                "    new EmptyObject()",
                ")"
                ).ToString(),
                new WellFormattedCode("new WithClassParam(new EmptyObject())").ToString(_output)
            );
        }

        [Fact]
        public void IfOneOfArgumentsWithNewShouldStartFromNewLine()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithClassAndIntParams(",
                "    42,",
                "    new EmptyObject()",
                ")"
                ).ToString(),
                new WellFormattedCode("new WithClassAndIntParams(42,new EmptyObject())").ToString(_output)
            );
        }

        [Fact]
        public void ComplexObjectsShouldBeWithInnerIndention()
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
                new WellFormattedCode(
                    "new WithTwoClassParamAndIntParam(new WithClassParam(new EmptyObject()),new WithClassAndIntParams(42,new EmptyObject()),42)"
                ).ToString(_output)

            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void LambdasShouldBePlacedOnNewLine()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithFuncArgument(",
                "    () => 0",
                ")"
                ).ToString(),
                new WellFormattedCode("new WithFuncArgument(() => 0)").ToString(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void LambdasWithArgumentShouldBePlacedOnNewLine()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithActionArgument(",
                "    pos => {}",
                ")"
                ).ToString(),
                new WellFormattedCode("new WithActionArgument(pos => {})").ToString(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void ArraysShouldBeFormatted_CtorWithIEnumerableInt()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithIEnumerableInt(",
                "    new[] ",
                "    {",
                "        1,",
                "        2",
                "    }",
                ")"
                ).ToString(),
                new WellFormattedCode("new WithIEnumerableInt(new[]{1,2})").ToString(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void DictionaryShouldBeFormatted_CtorWithDictionaryIntString()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "new WithDictionaryArgument(",
                "    new Dictionary<int,string>",
                "    {",
                "        { 1, \"1\" },",
                "        { 2, \"2\" },",
                "        { 3, \"3\" }",
                "    }",
                ")"
                ).ToString(),
                new WellFormattedCode(
                    "new WithDictionaryArgument(new Dictionary<int,string>{{ 1, \"1\" },{ 2, \"2\" },{ 3, \"3\" }})"
                ).ToString(_output)
            );
        }

        [Fact(Skip = "Need to be fixed in scope of puzzle #5")]
        public void ArgumentWithNewShouldBeFormatted_And_InnerPropertyShouldBeFromSeparateLine_TheSameObjectDetection()
        {
            Assert.Equal(
                new NewLineSeparatedString(
                "var user = new User(\"user name\");", Environment.NewLine +
                "new WithUserArgument(",
                "    user,",
                "    new WithUserPublicProperty()",
                "    {",
                "        User = user",
                "    }",
                ")"
                ).ToString(),
                new WellFormattedCode(
                    "var user = new User(\"user name\");new WithUserArgument(user,new WithUserPublicProperty(){User = user})"
                ).ToString(_output)
            );
        }
    }
}