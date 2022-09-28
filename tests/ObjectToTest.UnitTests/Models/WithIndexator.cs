using System;
namespace ObjectToTest.UnitTests.Models
{
    public class WithIndexator
    {
        private readonly int position;

        public WithIndexator(int position)
        {
            this.position = position;
        }

        public object this[string propertyName]
        {
            get
            {
                if (propertyName == "position")
                {
                    return position;
                }
                return 0;
            }
        }
    }
}
