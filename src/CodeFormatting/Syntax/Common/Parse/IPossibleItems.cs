namespace ObjectToTest.CodeFormatting.Syntax.Common.Parse
{
    public interface IPossibleItems<out TOut>
    {
        TOut BestMatch(string value);
    }
}