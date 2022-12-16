using System;
using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface ITransformationDefinition
    {
        void OverrideForArrayOf<T>(Func<object, bool> condition, Func<string[], string> format);

        void OverrideForArrayOf<T>(Func<string[], string> format);

        void OverrideForArrayOf<T>(Func<object, bool> condition, Func<string[], Tabs, (string, Tabs)> format);

        void If(Func<object, bool> condition, IObjectWithFormat format);

        void If(Func<object, bool> isApplicable, Func<string, string> transform);

        void If(Func<object, bool> isApplicable, Func<string, Tabs, (string, Tabs)> transform);
    }
}