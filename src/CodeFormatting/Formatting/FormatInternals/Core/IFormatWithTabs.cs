namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public interface IFormatWithTabs
    {
        (string, Tabs) Format(object item, Tabs tabs);
    }
}