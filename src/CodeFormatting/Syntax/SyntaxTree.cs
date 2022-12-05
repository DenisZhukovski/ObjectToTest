using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Implementation;

namespace ObjectToTest.CodeFormatting.Syntax
{
    /*
    * @todo #106 60m/DEV Adjust project structure.
     *
     * Folders should reflect SyntaxTree dependencies.
     * - CodeStatements
     * -- Instantiation
     * --- Arguments
     * --- Properties
     * -- Assignment
     * etc.
     *
     * All core classes should be placed into Core folder for SyntaxTree considering hierarchy if any - substrings, substring, character.
    */

    /*
    * @todo #106 60m/DEV Change split implementation to consider closures, lambdas and inline initializations.
     *
    */

    /*
    * @todo #106 60m/DEV Refactoring for all logic to be moved from constructor into lazy initializations or lazy executables.
     *
    */
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