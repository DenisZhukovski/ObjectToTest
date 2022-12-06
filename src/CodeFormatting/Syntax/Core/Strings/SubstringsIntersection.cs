namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
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
}