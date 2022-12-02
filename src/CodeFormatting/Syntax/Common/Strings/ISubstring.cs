namespace ObjectToTest.CodeFormatting.Syntax.Common.Strings
{
    public interface ISubstring
    {
        int Start { get; }
        
        int End { get; }

        string ToString();
    }
}