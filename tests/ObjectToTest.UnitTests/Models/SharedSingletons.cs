namespace ObjectToTest.UnitTests.Models
{
    public class SharedSingletons
    {
        public SharedSingletons(WithSingletonAndOtherArgument withSingletonAndOtherArgument, WithSingletonArgument withSingletonArgument)
        {
            WithSingletonAndOtherArgument = withSingletonAndOtherArgument;
            WithSingletonArgument = withSingletonArgument;
        }
        
        public WithSingletonAndOtherArgument WithSingletonAndOtherArgument { get; }
        
        public WithSingletonArgument WithSingletonArgument { get; }
    }
}
