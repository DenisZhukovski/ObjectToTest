using System;
using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Assignment
{
    public class PropertyAssignments : IPropertyAssignments
    {
        private readonly string _source;
        private readonly Lazy<PossibleAssignments> _possibleAssignments = new(() => new());

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
            foreach (var characterSeparatedSubstring in new CharacterSeparatedSubstrings(_source, ',', notAnalyzeIn: new LiteralsAndClosuresSubstrings(_source)))
            {
                yield return _possibleAssignments.Value.BestMatch(characterSeparatedSubstring.ToString());
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}