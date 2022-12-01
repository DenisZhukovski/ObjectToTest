using System;
using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Common;
using ObjectToTest.CodeFormatting.Syntax.Common.Strings;

namespace ObjectToTest.CodeFormatting.Syntax
{
    public interface ISyntaxTree : IEnumerable<ICodeStatement>
    {
    }
}