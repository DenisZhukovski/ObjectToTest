namespace ObjectToTest.CodeFormatting.Syntax.Core.CodeElements
{
    public class CodeCharacter
    {
        private readonly string _source;
        private readonly int _index;

        public CodeCharacter(string source, int index)
        {
            _source = source;
            _index = index;
        }

        public bool IsInBounds
        {
            get
            {
                if (_index >= 0 && _index < _source.Length)
                {
                    return true;
                }

                return false;
            }
        }

        public char Value
        {
            get
            {
                return _source[_index];
            }
        }

        public bool IsNotALexemChar
        {
            get
            {
                return !IsALexemChar;
            }
        }

        public bool IsALexemChar
        {
            get
            {
                return IsInBounds && (char.IsLetterOrDigit(Value) || Value == '.' || Value == '_');
            }
        }
    }
}