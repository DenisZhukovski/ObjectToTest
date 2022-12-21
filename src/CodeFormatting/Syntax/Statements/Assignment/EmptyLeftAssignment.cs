using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class EmptyLeftAssignment : ILeftAssignmentPart
    {
        public override string ToString()
        {
            return string.Empty;
        }
    }
}