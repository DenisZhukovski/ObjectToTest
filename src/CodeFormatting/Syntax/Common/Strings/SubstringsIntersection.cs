namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class SubstringsIntersection
    {
        private readonly ISubstring _source;
        private readonly ISubstring _target;

        public SubstringsIntersection(ISubstring source, ISubstring target)
        {
            _source = source;
            _target = target;
        }

        public bool SourceFullyInTarget
        {
            get
            {
                if (_source.Start >= _target.Start && _source.End <= _target.End)
                {
                    return true;
                }

                return false;
            }
        }
    }

    public class CharIndexIntersection
    {
        private readonly int _index;
        private readonly ISubstring _target;

        public CharIndexIntersection(int index, ISubstring target)
        {
            _index = index;
            _target = target;
        }

        public bool InTarget
        {
            get
            {
                if (_index >= _target.Start && _index <= _target.End)
                {
                    return true;
                }

                return false;
            }
        }
    }
}