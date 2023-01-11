namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class Item
    {
        private readonly string _description;

        public Item(object value) : this(value, 1, string.Empty)
        {
        }

        public Item(object value, int depth, string description)
        {
            Value = value;
            Depth = depth;
            _description = description;
        }

        public object Value { get; }

        public int Depth { get; }

        public override string ToString()
        {
            return (string.IsNullOrEmpty(_description) ? string.Empty : (_description + "-")) + $"{Value.GetType().Name}" + $"-[{Value}]";
        }
    }
}