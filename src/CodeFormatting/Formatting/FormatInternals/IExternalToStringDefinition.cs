using System;
using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface IExternalToStringDefinition
    {
        void For<T>(string format, Func<T, Args> args);

        void ForArrayOf<T>(Func<string[], string> format);
    }
}