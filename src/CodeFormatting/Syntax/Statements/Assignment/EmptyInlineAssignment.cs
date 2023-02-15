using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class EmptyInlineAssignment : IInlineAssignments
    {
        public override string ToString()
        {
            return "";
        }

        public IEnumerator<ICodeStatement> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}