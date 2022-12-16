namespace ObjectToTest.CodeFormatting.Formatting
{
    public class Parts
    {
        private readonly string[] _items;

        public Parts(params string[] items)
        {
            _items = items;
        }

        public override string ToString()
        {
            return string.Join("", _items);
        }
    }
}