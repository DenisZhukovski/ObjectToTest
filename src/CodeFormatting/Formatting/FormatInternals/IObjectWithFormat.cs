namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public interface IObjectWithFormat
    {
        public string Format(object item);

        public object[] Args(object item);
    }
}