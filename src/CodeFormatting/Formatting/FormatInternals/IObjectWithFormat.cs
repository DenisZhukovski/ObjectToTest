namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public interface IObjectWithFormat
    {
        public (string, Tabs) Format(object item, Tabs tabs);

        public object[] Args(object item);
    }
}