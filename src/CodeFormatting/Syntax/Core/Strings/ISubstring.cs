namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public interface ISubstring
    {
        int Start { get; }
        
        int End { get; }

        string ToString();
    }
}