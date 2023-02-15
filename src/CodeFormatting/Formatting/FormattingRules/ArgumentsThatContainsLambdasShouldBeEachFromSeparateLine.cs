using System.Collections.Generic;
using System.Linq;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class ArgumentsThatContainsLambdasShouldBeEachFromSeparateLine : IFormattingRule
    {
        public void ApplyTo(IFormat definition)
        {
            definition.OverrideForArrayOf<IArgument>(
                x => x is IEnumerable<IArgument> arguments && arguments.Any(x => x is ILambda),
                (arguments, tabs) => new FormatWithSameLineOpenBracketAndEachArgFromNewLine(arguments, tabs).Format(),
                name: nameof(ArgumentsThatContainsLambdasShouldBeEachFromSeparateLine)
            );
        }
    }
}