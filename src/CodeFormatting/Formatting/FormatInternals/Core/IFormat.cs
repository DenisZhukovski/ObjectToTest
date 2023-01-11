using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface IFormat
    {
        string ApplyTo(object item);

        void Add(INodeFormat format);

        void AddAsFirst(INodeFormat format);

        void AddAsFirst(INodeTransformation transformation);

        void Add(INodeTransformation transformation);
    }
}