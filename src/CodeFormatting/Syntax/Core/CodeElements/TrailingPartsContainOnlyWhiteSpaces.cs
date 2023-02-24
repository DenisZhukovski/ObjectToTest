using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class TrailingPartsContainOnlyWhiteSpaces
    {
        private readonly string _source;
        private readonly ISubstring _closure;

        public TrailingPartsContainOnlyWhiteSpaces(string source, ISubstring closure)
        {
            _source = source;
            _closure = closure;
        }

        public bool IsTrue
        {
            get
            {
                for (var i = 0; i < _source.Length; i++)
                {
                    if (i < _closure.Start && _source[i] != ' ')
                    {
                        return false;
                    }

                    if (i > _closure.End && _source[i] != ' ')
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}