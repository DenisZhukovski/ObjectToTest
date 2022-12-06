using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Core.CodeElements;

namespace ObjectToTest.CodeFormatting.Syntax.Core.Strings
{
    public static class MatchingExtensions
    {
        public static IEnumerable<ISubstring> ExcludeOnesThatAreIn(this IEnumerable<ISubstring> source, IEnumerable<ISubstring> target) => new ExcludeSubstrings(source, target);

        public static IEnumerable<ISubstring> ExcludeNotSeparateWords(this IEnumerable<ISubstring> matches, string source) => new SeparateWordsSubstrings(source, matches);

        public static ClosureSubstringWithoutBorders WithoutBorders(this ISubstring substring) => new(substring);
    }
}