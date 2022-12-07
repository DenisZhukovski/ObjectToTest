using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class Args : IEnumerable<object>
    {
        private readonly object[] _args;

        public Args(params object[] args)
        {
            _args = args;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return _args.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}