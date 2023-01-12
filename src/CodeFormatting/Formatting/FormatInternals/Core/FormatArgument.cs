namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class FormatArgument
    {
        private readonly string _description;

        public FormatArgument(object value) : this(value, 1, string.Empty)
        {
        }

        public FormatArgument(object value, int depth, string description)
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