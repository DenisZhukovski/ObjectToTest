using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class SubstringBetween : ISubstring
    {
        private readonly string _source;
        private readonly LiteralAwareSubstring _left;
        private readonly LiteralAwareSubstring _right;

        public SubstringBetween(string source, string left, string right)
            : this(
                source,
                new LiteralAwareSubstring(source, left),
                new LiteralAwareSubstring(source, right)
            )
        {
        }
        
        public SubstringBetween(string source, LiteralAwareSubstring left, LiteralAwareSubstring right)
        {
            _source = source;
            _left = left;
            _right = right;
        }

        public int Start
        {
            get
            {
                var leftSubString = _left.FirstOrDefault();
                if (leftSubString != null)
                {
                    return leftSubString.End + 1;
                }

                return -1;
            }
        }

        public int End
        {
            get
            {
                var rightSubString = _right.FirstOrDefault();
                if (rightSubString != null)
                {
                    return rightSubString.Start - 1;
                }

                return -1;
            }
        }

        public override string ToString()
        {
            if (Start == -1 || End == -1)
            {
                return string.Empty;
            }
            
            return new Substring(_source, Start, End).ToString();
        }
    }
}