namespace ObjectToTest.UnitTests.Data
{
    public class WithGenericArgument<T>
    {
        public T Create()
        {
            return default(T);
        }
    }
}
