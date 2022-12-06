using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

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

        public static IPropertyAssignments Parse(string codeStatement)
        {
            var propertiesClosure = new LiteralAwareClosureSubstrings(codeStatement, '{', '}').ToArray();
            if (propertiesClosure.Any())
            {
                return new PropertyAssignments(propertiesClosure.First().WithoutBorders().ToString());
            }
            else
            {
                return new EmptyPropertyAssignment();
            }
        }
    }
}