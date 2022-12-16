using System;
using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface ITransformationDefinition
    {
        /// <summary>
        /// Transformation applied only once.
        /// i.e. '{0},{1}' and then '{0};{1}', only latest '{0};{1}' would be applied.
        /// Scope of appliance should be managed by condition from most abstract to less abstract.
        ///
        /// Tabs as an incoming parameters - tabs of parent.
        /// Tabs as an outgoing parameters - tabs of elements children.
        /// </summary>
        void OverrideForArrayOf<T>(Func<object, bool> condition, Func<string[], string> format);

        /// <summary>
        /// Transformation applied only once.
        /// i.e. '{0},{1}' and then '{0};{1}', only latest '{0};{1}' would be applied.
        /// Implicit type check is presented.
        /// </summary>
        void OverrideForArrayOf<T>(Func<string[], string> format);

        /// <summary>
        /// Transformation applied only once.
        /// i.e. '{0},{1}' and then '{0};{1}', only latest '{0};{1}' would be applied.
        /// Scope of appliance should be managed by condition from most abstract to less abstract.
        /// </summary>
        void OverrideForArrayOf<T>(Func<object, bool> condition, Func<string[], Tabs, (string, Tabs)> format);

        /// <summary>
        /// Transformation is applied in stack for each applicable item.
        /// i.e. '{0}' => ' {0}' and then '{0}' => '-{0}', result would be '- {0}'.
        /// </summary>
        void If(Func<object, bool> condition, IObjectWithFormat format);

        /// <summary>
        /// Transformation is applied in stack for each applicable item.
        /// i.e. '{0}' => ' {0}' and then '{0}' => '-{0}', result would be '- {0}'.
        /// </summary>
        void If(Func<object, bool> isApplicable, Func<string, string> transform);

        /// <summary>
        /// Transformation is applied in stack for each applicable item.
        /// i.e. '{0}' => ' {0}' and then '{0}' => '-{0}', result would be '- {0}'.
        ///
        /// Tabs as an incoming parameters - tabs of parent.
        /// Tabs as an outgoing parameters - tabs of elements children.
        /// </summary>
        void If(Func<object, bool> isApplicable, Func<string, Tabs, (string, Tabs)> transform);
    }
}