using System;
namespace ObjectToTest.UnitTests.Models
{
    public class WithCustomDataExtended
    {
        private readonly WithCustomData data;
        private readonly WithCustomHashCode withCustomHashCode;

        public WithCustomDataExtended(WithCustomData data, WithCustomHashCode withCustomHashCode)
        {
            this.data = data;
            this.withCustomHashCode = withCustomHashCode;
        }
    }
}
