using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Args
{
    public class SkippedArguments : IArguments
    {
        public override string ToString()
        {
            return "";
        }

        public IEnumerator<IArgument> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}