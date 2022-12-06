using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class SubstringBetween : ISubstring
    {
        private readonly string _source;

        public SubstringBetween(string source, string left, string right)
        {
            _source = source;
            var leftKeyword = new LiteralAwareSubstring(source, left).First();
            var rightKeyword = new LiteralAwareSubstring(source, right).First();

            Start = leftKeyword.End + 1;
            End = rightKeyword.Start - 1;
        }

        public int Start { get; }

        public int End { get; }

        public override string ToString()
        {
            return new Substring(_source, Start, End).ToString();
        }
    }
}