using System;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public class NewLineSeparatedString
    {
        private readonly string[] _lines;

        public NewLineSeparatedString(params string[] lines)
        {
            _lines = lines;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _lines);
        }
    }

    public class JointString
    {
        private readonly string[] _lines;

        public JointString(params string[] lines)
        {
            _lines = lines;
        }

        public override string ToString()
        {
            return string.Join("", _lines);
        }
    }
}