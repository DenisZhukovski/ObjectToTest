﻿using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Formatting.Core;
using ObjectToTest.UnitTests.Models.ForFormatting;
using Xunit;

namespace ObjectToTest.UnitTests
{
    public class FormatTests
    {
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

            var f = new Format();
            f.For<A>("{0} {1}", x => new Args(x.I, x.Inner));
            f.For<B>("({0}) [{1}]", x => new Args(x.J, x.Arr));
            f.ForArrayOf<C>(x => string.Join(", ", x));
            f.For<C>("{0}", x => new Args(x.K));
            f.If(x => x is A, x => $"  {x}");
            f.If(x => x is B, x => $"={x}=");
            f.If(x => x is C c && c.K == 1, x => $"-{x}-");
            f.ApplyTo(a)
                .ClaimEqual("  42 =(22) [-1-, 2]=");
        }
    }
}