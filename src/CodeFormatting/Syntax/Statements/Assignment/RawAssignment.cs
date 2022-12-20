using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class RawAssignment : IAssignmentPart
    {
        private readonly string _source;

        public RawAssignment(string source)
        {
            _source = source;
        }

        public ILeftAssignmentPart Left { get; } = new EmptyLeftAssignment();

        public IRightAssignmentPart Right { get; } = new EmptyRightAssignment();

        public override string ToString()
        {
            return _source.Trim();
        }
    }
}