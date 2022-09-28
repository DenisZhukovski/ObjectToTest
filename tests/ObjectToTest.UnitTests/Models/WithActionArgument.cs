using System;

namespace ObjectToTest.UnitTests.Models
{
    public class WithActionArgument
    {
        private readonly Action<int> setPosition;

        public WithActionArgument(Action<int> setPosition)
        {
            this.setPosition = setPosition;
        }

        public void SetPosition(int position)
        {
            this.setPosition(position);
        }
    }
}
