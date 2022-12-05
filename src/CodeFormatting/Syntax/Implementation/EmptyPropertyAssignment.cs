﻿using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class EmptyPropertyAssignment : IPropertyAssignments
    {
        public override string ToString()
        {
            return "";
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