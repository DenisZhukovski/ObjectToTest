namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
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