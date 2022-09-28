using System;
namespace ObjectToTest.UnitTests.Models
{
    public class WithFuncArgument
    {
        private readonly Func<int> getPosition;

        public WithFuncArgument(Func<int> getPosition)
        {
            this.getPosition = getPosition;
        }

        public int Position()
        {
            return this.getPosition();
        }
    }
}
