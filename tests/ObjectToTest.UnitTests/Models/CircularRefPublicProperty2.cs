namespace ObjectToTest.UnitTests.Models
{
    public class CircularRefPublicProperty2
    {
        public CircularRefPublicProperty1? PropertyName1 { get; set; }
        public CircularRefPublicProperty3? PropertyName3 { get; set; }
    }
}
