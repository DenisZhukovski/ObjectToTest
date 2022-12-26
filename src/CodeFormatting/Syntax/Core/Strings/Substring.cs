namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class Substring : ISubstring
    {
        private readonly string _source;

        public Substring(string source)
            : this(source, 0, source.Length - 1)
        {
        }
        
        public Substring(string source, int start, int end)
        {
            Start = start;
            End = end;
            _source = source;
        }

        public int Start { get; }

        public int End { get; }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj)
                   || obj is ISubstring substring && substring.ToString() == ToString()
                   || obj is string s && s == ToString();
        }

        public override int GetHashCode()
        {
#if NETSTANDARD2_1
            return System.HashCode.Combine(_source, Start, End);
#else
            return new { _source, Start, End }.GetHashCode();
#endif
        }

        public override string ToString()
        {
            return _source.Substring(Start, End - Start + 1);
        }
    }
}