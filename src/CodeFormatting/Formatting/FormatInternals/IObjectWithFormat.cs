namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public interface IObjectWithFormat : IFormatWithTabs
    {
        object[] Args(object item);
    }
}