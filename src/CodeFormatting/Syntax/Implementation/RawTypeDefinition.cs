using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class RawTypeDefinition : ITypeDefinition
    {
        private readonly string _code;

        public RawTypeDefinition(string code)
        {
            _code = code;
        }

        public override string ToString()
        {
            return _code;
        }
    }
}