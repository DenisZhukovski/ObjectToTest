namespace ObjectToTest.UnitTests.Data
{
    public class With2GenericArguments<T, T2>
    {
        private readonly T _item;

        public With2GenericArguments(T item)
        {
            this._item = item;
        }

        public T? Create()
        {
            return default(T);
        }

        public T2? Create2()
        {
            return default(T2);
        }
    }
}
