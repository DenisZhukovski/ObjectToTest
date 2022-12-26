using System;
using ObjectToTest.Extensions;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class FormatWithHeaderAndEachFromNewLine
    {
        private readonly Func<string[], string> _header;
        private readonly string[] _args;
        private readonly Tabs _tabs;

        public FormatWithHeaderAndEachFromNewLine(Func<string[], string> header, string[] args, Tabs tabs)
        {
            _header = header;
            _args = args;
            _tabs = tabs;
        }

        public (string, Tabs) Format()
        {
            var format = new Parts(
                _tabs.Tab() + _header(_args) + Environment.NewLine,
                _args.FormatEach(argument => $"{_tabs.Tab().Tab()}- Argument: {argument.String}{Environment.NewLine.If(argument.IsNotLast)}").ToString()
            );

            return (format.ToString(), _tabs.Tab().Tab());
        }
    }
}