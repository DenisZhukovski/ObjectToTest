namespace ObjectToTest.UnitTests.Models
{
    public class WithCustomHashCode
    {
        private readonly string data;
        private readonly int hashCode;

        public WithCustomHashCode(string data, int hashCode)
        {
            this.data = data;
            this.hashCode = hashCode;
        }

        public override bool Equals(object? obj)
        {
            if (obj is WithCustomHashCode customData)
            {
                return this.data.Equals(customData.data); 
            }
            return this.data.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }
    }
}
