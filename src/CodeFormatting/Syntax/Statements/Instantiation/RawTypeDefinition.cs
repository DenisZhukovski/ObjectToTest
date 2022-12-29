using ObjectToTest.CodeFormatting.Syntax.Contracts;
using ObjectToTest.CodeFormatting.Syntax.Core.Strings;

namespace ObjectToTest.CodeFormatting.Syntax.Statements.Instantiation
{
    public class RawTypeDefinition : ITypeDefinition
    {
        private readonly ISubstring _typeAsString;

        public RawTypeDefinition(string code)
            : this(
                new FirstOf(
                    new SubstringBetween(code, "new ", "("),
                    new SubstringBetween(code, "new", "{")
                )
            )
        {
        }

        public RawTypeDefinition(ISubstring typeAsString)
        {
            _typeAsString = typeAsString;
        }

        public override string ToString()
        {
            return _typeAsString.ToString().Trim();
        }
    }
}