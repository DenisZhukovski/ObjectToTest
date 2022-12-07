using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class PropertyAssignments : IPropertyAssignments
    {
        /*
        * @todo #103 60m/DEV Parse properties.
        */

        private readonly string _source;

        public PropertyAssignments(string source)
        {
            _source = source;
        }

        public override string ToString()
        {
            return _source;
        }

        public IEnumerator<IAssignmentPart> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}