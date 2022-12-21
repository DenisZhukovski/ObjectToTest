using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class EmptyRightAssignment : IRightAssignmentPart
    {
        public override string ToString()
        {
            return string.Empty;
        }
    }
}