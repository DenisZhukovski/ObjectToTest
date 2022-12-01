using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class SyntaxTree : ISyntaxTree
    {
        private readonly string _code;
        private readonly PossibleCodeStatements _codeStatements = new();

        public SyntaxTree(string code)
        {
            _code = code;
        }

        public IEnumerator<ICodeStatement> GetEnumerator()
        {
            foreach (var s in new StringsWithSemicolonIfMultiline(_code))
            {
                yield return _codeStatements.BestMatch(s);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}