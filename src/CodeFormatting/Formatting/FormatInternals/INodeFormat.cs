namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public interface INodeFormat
    {
        bool IsApplicableFor(object item);

        object[] Args(object item);

        (string, Tabs) Apply(object item, Tabs tabs);
    }
}