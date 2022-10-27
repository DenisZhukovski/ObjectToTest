using System;
using Xunit;
using Xunit.Abstractions;

namespace ObjectToTest.UnitTests
{
    public class MethodBodyTests
    {
        private readonly ITestOutputHelper _output;

        public MethodBodyTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public void MethodBodyReturns0()
        {
            var method = new Func<int>(() => 0);
            Assert.Equal(
                "0000 : ldc.i4.0\n" +
                "0001 : ret", 
                new ObjectToTest.ILReader.MethodBody(method.Method).ToString().Log(_output)
            );
        }

        [Fact]
        public void MethodBodyUpdatesLocals()
        {
            var count = 0;
            var data = string.Empty;
            var method = new Action<int, string>((i, str) =>
            {
                count = i;
                data = str;
            });
            Assert.Equal(
                "0000 : nop\n"+
                "0001 : ldarg.0\n"+
                "0002 : ldarg.1\n"+
                "0003 : stfld int ObjectToTest.UnitTests.MethodBodyTests+<>c__DisplayClass3_0::count\n"+
                "0008 : ldarg.0\n"+
                "0009 : ldarg.2\n"+
                "0010 : stfld string ObjectToTest.UnitTests.MethodBodyTests+<>c__DisplayClass3_0::data\n"+
                "0015 : ret",
                new ILReader.MethodBody(method.Method).ToString().Log(_output)
            );
        }
    }
}