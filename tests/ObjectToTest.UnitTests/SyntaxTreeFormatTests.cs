﻿using ObjectToTest.CodeFormatting.Formatting.FormattedCodeInternals;
using ObjectToTest.CodeFormatting.Syntax;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class SyntaxTreeFormatTests
    {
        [Fact]
        public void Clear()
        {
            new SyntaxTreeFormat()
                .ApplyTo(
                    new SyntaxTree(
                        "new Foo(1,233)  ; new   Bar(1,  2)"
                    )
                ).ClaimEqual("new Foo(1,233);new Bar(1,2)");
        }
    }
}