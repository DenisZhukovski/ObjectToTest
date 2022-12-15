using System;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.Extensions;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class ArgumentsBiggerThan80SpacesShouldBeFormatted : IFormattingRule
    {
        public void ApplyTo(ITransformationDefinition definition)
        {
            definition.OverrideForArrayOf<IArgument>(
                x => x.ToString().Length > 80,
                (arguments, tabs) =>
                {
                    var format = new Parts(
                        "(" + Environment.NewLine,
                        arguments.FormatEach(argument => $"{tabs.Tab()}{argument.String}{",".If(argument.IsNotLast)}{Environment.NewLine}").ToString(),
                        tabs + ")" + Environment.NewLine
                    );

                    return (format.ToString(), tabs.Tab());
                }
            );
        }
    }
}