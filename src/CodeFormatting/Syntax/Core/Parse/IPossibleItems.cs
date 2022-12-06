namespace ObjectToTest.CodeFormatting.Syntax.Core.Parse
{
    public interface IPossibleItems<out TOut>
    {
        TOut BestMatch(string value);
    }
}