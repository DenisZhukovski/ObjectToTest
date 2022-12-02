using System.Collections;
using System.Collections.Generic;
using ObjectToTest.CodeFormatting.Syntax.Contracts;

namespace ObjectToTest.CodeFormatting.Syntax.Implementation
{
    public class Arguments : IArguments
    {
        /*
        * @todo #103 60m/DEV Parse arguments.
        */

        private readonly string _source;

        public Arguments(string source)
        {
            _source = source;
        }

        public override string ToString()
        {
            return _source;
        }

        public IEnumerator<IArgument> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}