using System.Collections;
using System.Collections.Generic;

namespace ObjectToTest.CodeFormatting.Syntax
{
    public interface ISyntaxTree : IEnumerable<ICodeStatement>
    {
        /*
         * @todo #91 60m/DEV Create an implementation for this.
         * It should be responsible for splitting code to statements and providing statements of the right type.
         */
    }
}