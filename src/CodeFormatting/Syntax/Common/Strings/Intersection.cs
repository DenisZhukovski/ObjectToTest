namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public class Intersection
    {
        private readonly ISubstring _source;
        private readonly ISubstring _target;

        public Intersection(ISubstring source, ISubstring target)
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
}