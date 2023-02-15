using System;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class EachNewStatementShouldBeFromSeparateLine : IFormattingRule
    {
        public void ApplyTo(IFormat definition)
        {
            definition.OverrideForArrayOf<ICodeStatement>(
                x => new Parts(
                    string.Join(";" + Environment.NewLine, x)
                ).ToString(),
                name: nameof(EachNewStatementShouldBeFromSeparateLine)
            );
        }
    }
}