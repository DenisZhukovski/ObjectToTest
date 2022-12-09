namespace ObjectToTest.UnitTests.Models
{
    public class With2ObjectArguments
    {
        private readonly object _item1;
        private readonly object _item2;

        public With2ObjectArguments(object item1, object item2)
        {
            _item1 = item1;
            _item2 = item2;
        }

        public string Foo()
        {
            return _item1.ToString() + _item2.ToString();
        }
    }
}