using System.Collections.Generic;
using ObjectToTest.Arguments;

namespace ObjectToTest.Constructors
{
    public class NullConstructor : IConstructor
    {
        private List<IArgument>? _arguments;

        public bool IsValid => true;

        public IList<IArgument> Arguments => _arguments ??= new List<IArgument>();
        
        public object? Object => null;

        public string Type => "null";

        public override string ToString()
        {
            return "null";
        }
    }
}