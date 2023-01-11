using System;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public sealed class NodeTransformation : INodeTransformation
    {
        public Func<string, Tabs, (string, Tabs)>? Format { get; set; }

        public Func<object, bool>? IsApplicable { get; set; }

        public string Name { get; set; }

        public bool IsApplicableFor(object item)
        {
            return IsApplicable(item);
        }

        (string, Tabs) INodeTransformation.Apply(string item, Tabs tabs)
        {
            return Format(item, tabs);
        }
    }
}