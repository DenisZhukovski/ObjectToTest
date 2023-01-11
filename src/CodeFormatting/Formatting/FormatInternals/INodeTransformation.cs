namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public interface INodeTransformation
    {
        bool IsApplicableFor(object item);

        (string, Tabs) Apply(string item, Tabs tabs);
    }
}