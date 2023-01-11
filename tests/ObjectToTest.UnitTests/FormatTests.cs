using Microsoft.VisualStudio.TestPlatform.Utilities;
using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.UnitTests.Extensions;
using ObjectToTest.UnitTests.Models.ForFormatting;
using System;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class FormatTests
    {
        private readonly ITestOutputHelper _output;

        public FormatTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ToStringDefinition_Arrays_Transformations()
        {
            var a = new A()
            {
                I = 42,
                Inner = new B()
                {
                    J = 22,
                    Arr = new C[]
                    {
                        new C()
                        {
                            K = 1
                        },
                        new C()
                        {
                            K = 2
                        }
                    }
                }
            };
            
            var f = new Format(new FormatLoggerForTests(_output));
            f.For<A>("{0} {1}", x => new Args(x.I, x.Inner), "Base A format");
            f.For<B>("({0}) {1}", x => new Args(x.J, x.Arr), "Base B format");
            f.ForArrayOf<C>(x => string.Join(", ", x), "Base C[] format");
            f.For<C>("{0}", x => new Args(x.K), "Base C format");
            f.If(x => x is C[], x => $"[{x}]", "Wrap C[]");
            f.If(x => x is A, x => $"  {x}", "Indent A");
            f.If(x => x is B, x => $"={x}=", "Wrap = B");
            f.If(x => x is C c && c.K == 1, x => $"-{x}-", "Wrap C in -");
            f.ApplyTo(a)
                .ClaimEqual("  42 =(22) [-1-, 2]=");
        }

        [Fact]
        public void FormatWithIndentations()
        {
            var a = new A()
            {
                I = 42,
                Inner = new B()
                {
                    J = 22,
                    Arr = new C[]
                    {
                        new C()
                        {
                            K = 1
                        },
                        new C()
                        {
                            K = 2
                        }
                    }
                }
            };
            
            var f = new Format(new FormatLoggerForTests(_output));
            f.For<A>("{0}{1}", x => new Args(x.I, x.Inner), "Base A format");
            f.For<B>("{0}{1}", x => new Args(x.J, x.Arr), "Base B format");
            f.ForArrayOf<C>(x => string.Join(Environment.NewLine, x), "Base C[] format");
            f.For<C>("{0}", x => new Args(x.K), "Base C format");
            f.If(x => x is B, (x, parentTabs) => ($"{Environment.NewLine}{parentTabs.Tab()}{x}", parentTabs.Tab()), "Make B from new line");
            f.OverrideForArrayOf<C>(x => x is C[], (x, parentTabs) =>
                {
                    return (
                        $"{Environment.NewLine}" +
                        $"{parentTabs}{{{{{Environment.NewLine}" +
                        $"{new Join(x, i => $"{parentTabs.Tab()}{i.String}{Environment.NewLine}")}" +
                        $"{parentTabs}}}}}",
                        parentTabs.Tab());
                }
            , "Format C[] properly");
            f.ApplyTo(a)
                .ClaimEqual(new NewLineSeparatedString(
                    "42",
                    "    22",
                    "    {",
                    "        1",
                    "        2",
                    "    }").ToString());
        }
    }

    
}