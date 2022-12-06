namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class InBoundIndex
    {
        private readonly string _source;
        private readonly int _index;

        public InBoundIndex(string source, int index)
        {
            _source = source;
            _index = index;
        }

        public int Value
        {
            get
            {
                if (_index < 0)
                {
                    return 0;
                }

                if (_index >= _source.Length)
                {
                    return _source.Length - 1;
                }

                return _index;
            }
        }
    }
}