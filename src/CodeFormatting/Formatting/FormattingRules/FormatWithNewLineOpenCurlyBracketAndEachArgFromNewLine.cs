using System;
using ObjectToTest.Extensions;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class FormatWithNewLineOpenCurlyBracketAndEachArgFromNewLine
    {
        private readonly string[] _args;
        private readonly Tabs _tabs;

        public FormatWithNewLineOpenCurlyBracketAndEachArgFromNewLine(string[] args, Tabs tabs)
        {
            _args = args;
            _tabs = tabs;
        }

        public (string, Tabs) Format()
        {
            var format = new Parts(
                Environment.NewLine + _tabs + "{{" + Environment.NewLine,
                _args.FormatEach(argument => $"{_tabs.Tab()}{argument.String}{",".If(argument.IsNotLast)}{Environment.NewLine}").ToString(),
                _tabs + "}}"
            );

            return (format.ToString(), _tabs.Tab());
        }
    }
}