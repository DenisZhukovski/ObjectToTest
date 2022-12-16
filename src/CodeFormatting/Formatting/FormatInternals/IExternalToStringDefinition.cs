using System;
using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface IExternalToStringDefinition
    {
        /// <summary>
        /// Only last format is applied.
        /// </summary>
        void For<T>(string format, Func<T, Args> args);

        /// <summary>
        /// Only last format is applied.
        /// </summary>
        void ForArrayOf<T>(Func<string[], string> format);
    }
}