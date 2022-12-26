using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;
using ObjectToTest.CodeFormatting.Syntax.Statements;

namespace ObjectToTest.CodeFormatting.Syntax
{
    /*
    * @todo #106 60m/DEV Change split implementation to consider closures, lambdas and inline initializations.
     *
    */
    public class SyntaxTree : ISyntaxTree
    {
        private readonly string _code;

        private readonly Lazy<PossibleCodeStatements> _codeStatements = new(() => new PossibleCodeStatements());

        public SyntaxTree(string code)
        {
            _code = code;
        }

        public IEnumerator<ICodeStatement> GetEnumerator()
        {
            foreach (var s in new StringsWithSemicolonIfMultiline(_code))
            {
                yield return _codeStatements.Value.BestMatch(s);
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var statement in this)
            {
                result.AppendLine(statement.ToString());
            }
            return result.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}