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
            return this.data.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }
    }
}
