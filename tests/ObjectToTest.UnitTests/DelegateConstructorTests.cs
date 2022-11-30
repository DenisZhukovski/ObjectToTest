using System;
using ObjectToTest.Constructors;
using ObjectToTest.UnitTests.Data;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class DelegateConstructorTests
    {
        private readonly ITestOutputHelper _output;

        public DelegateConstructorTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void FuncCtor()
        {
            Assert.Equal(
                "() => 0",
                new DelegateConstructor(new Func<int>(() => 0))
                    .ToString()
                    .Log(_output)
            ); 
        }
        
        [Fact]
        public void FuncCtorWith1Param()
        {
            Assert.Equal(
                "i => 0",
                new DelegateConstructor(new Func<int, int>(i => 0))
                    .ToString()
                    .Log(_output)
            ); 
        }

        [Fact]
        public void ActionCtorWith1Param()
        {
            Assert.Equal(
                "_ => {}",
                new DelegateConstructor(new Action<int>(_ => { }))
                    .ToString()
                    .Log(_output)
            );
        }
        
        [Fact]
        public void ActionCtorNoParams()
        {
            Assert.Equal(
                "() => {}",
                new DelegateConstructor(new Action(() => { }))
                    .ToString()
                    .Log(_output)
            );
        }
        
        [Fact]
        public void ActionCtorWith2Params()
        {
            Assert.Equal(
                "(i, str) => {}",
                new DelegateConstructor(new Action<int, string>((i, str) => { }))
                    .ToString()
                    .Log(_output)
            );
        }
        
        [Fact]
        public void Type()
        {
            Assert.Equal(
                "Action`2",
                new DelegateConstructor(new Action<int, string>((i, str) => { })).Type
            );
        }
        
        
        [Fact]
        public void Object()
        {
            var item = new Action<int, string>((i, str) => { });
            Assert.Equal(
                item,
                new DelegateConstructor(item).Object
            );
        }
        
        [Fact(Skip = "Need to be fixed")]
        public void ActionCtorWith2ParamsAndBody()
        {
            /*
            * @todo #43:60m/DEV Make ActionCtorWith2ParamsAndBody test to be green.
            * Now DelegateConstructor does not support delegate body generation.
             * DelegateConstructor class should be able to generate the body for the Func
            */
            
            var count = 0;
            var data = string.Empty;
            Assert.Equal(
                $"var count = 0;{Environment.NewLine}" +
                $"var data = string.Empty;{Environment.NewLine}" +
                $"(i, str) => {{{Environment.NewLine}" +
                $"\tcount = i;{Environment.NewLine}" +
                $"\tdata = str;{Environment.NewLine}" +
                $"}}{Environment.NewLine}",
                new DelegateConstructor(
                        new Action<int, string>((i, str) => 
                        { 
                            count = i;
                            data = str;
                        }))
                    .ToString()
                    .Log(_output)
            );
        }
    }
}