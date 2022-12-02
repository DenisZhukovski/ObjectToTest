namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class SubstringWithoutOutOfBounds : ISubstring
    {
        private readonly string _source;

        public SubstringWithoutOutOfBounds(string source, int start, int length)
        {
            _source = source;
            Start = start;
            End = start + length - 1;
        }

        public int Start { get; }

        public int End { get; }

        public override string ToString()
        {
            var start = new InBoundIndex(_source, Start);
            var end = new InBoundIndex(_source, End);

            return new Substring(_source, start.Value, end.Value).ToString();
        }
    }
}