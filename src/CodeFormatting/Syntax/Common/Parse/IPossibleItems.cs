namespace ObjectToTest.CodeFormatting.Syntax.Common
{
    public interface IPossibleItems<TOut>
    {
        TOut BestMatch(string value);
    }
}