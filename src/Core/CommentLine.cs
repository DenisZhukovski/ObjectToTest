using System.Collections.Generic;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;

namespace ObjectToTest
{
    public class CommentLine : IConstructor
    {
        private readonly string _comment;

        public CommentLine(string comment)
        {
            _comment = comment;
        }

        public bool IsValid => true;
        public IList<IArgument> Argumetns => new List<IArgument>();

        public override string ToString()
        {
            return $"// {_comment}";
        }
    }
}