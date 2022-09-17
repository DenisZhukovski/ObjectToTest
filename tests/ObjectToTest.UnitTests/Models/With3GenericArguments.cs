namespace ObjectToTest.UnitTests.Data
{
    public class With3GenericArguments<T, T2, T3>
    {
        public T? Create()
        {
            return default(T);
        }

        public T2? Create2()
        {
            return default(T2);
        }

        public T3? Create3()
        {
            return default(T3);
        }
    }
}
