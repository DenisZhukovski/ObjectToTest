namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class Substring : ISubstring
    {
        private readonly string _source;

        public Substring(string source, int start, int end)
        {
            Start = start;
            End = end;
            _source = source;
        }

        public int Start { get; }

        public int End { get; }

        public override string ToString()
        {
            return _source.Substring(Start, End - Start + 1);
        }
    }
}