using System;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public sealed class NodeFormat : INodeFormat
    {
        public IObjectWithFormat? Format { get; set; }

        public Func<object, bool>? IsApplicable { get; set; }

        public string? Name { get; set; }

        public bool IsApplicableFor(object item)
        {
            return IsApplicable?.Invoke(item) ?? false;
        }

        public object[] Args(object item)
        {
            return Format.Args(item);
        }

        (string, Tabs) INodeFormat.Apply(object item, Tabs tabs)
        {
            return Format.Format(item, tabs);
        }
    }
}