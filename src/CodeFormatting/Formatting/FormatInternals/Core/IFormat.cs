using ObjectToTest.CodeFormatting.Formatting.Core;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public interface IFormat
    {
        string ApplyTo(object item);

        void Add(INodeFormat format);

        void Add(INodeTransformation transformation);
        
        void AddAsFirst(INodeFormat format);

        void AddAsFirst(INodeTransformation transformation);
    }
}