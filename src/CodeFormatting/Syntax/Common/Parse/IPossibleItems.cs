namespace ObjectToTest.CodeFormatting.Syntax.Common.Parse
{
    public interface IPossibleItems<TOut>
    {
        TOut BestMatch(string value);
    }
}