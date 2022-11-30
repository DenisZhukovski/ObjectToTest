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

        bool IConstructor.IsValid => true;

        IList<IArgument> IConstructor.Arguments => new List<IArgument>();
        
        object? IConstructor.Object => null;

        public override string ToString()
        {
            return $"// {_comment}";
        }
    }
}