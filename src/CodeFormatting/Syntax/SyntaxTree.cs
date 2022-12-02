using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Implementation;

namespace ObjectToTest.CodeFormatting.Syntax
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