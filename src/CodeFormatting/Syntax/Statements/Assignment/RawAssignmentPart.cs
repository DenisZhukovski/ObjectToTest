using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class RawAssignmentPart : ILeftAssignmentPart, IRightAssignmentPart
    {
        private readonly string _code;

        public RawAssignmentPart(string code)
        {
            _code = code;
        }

        public override string ToString()
        {
            return _code;
        }
    }
}