using ObjectToTest.CodeFormatting.Formatting;
using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Dump;

namespace ObjectToTest.CodeFormatting.Syntax.Extensions
{
    public static class SyntaxTreeExtensions
    {
        public static string Dump(this ISyntaxTree syntaxTree)
        {
            return new SyntaxTreeDump(syntaxTree, new Tabs(0)).ToString();
        }
    }
}