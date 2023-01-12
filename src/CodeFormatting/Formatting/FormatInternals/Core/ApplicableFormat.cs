using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class ApplicableFormat
    {
        private readonly object _arg;
        private readonly List<INodeFormat> _formats;

        public ApplicableFormat(object arg, List<INodeFormat> formats)
        {
            _arg = arg;
            _formats = formats;
        }

        public bool IsAvailable => _formats.Any(x => x.IsApplicableFor(_arg));

        public INodeFormat ToApply => _formats.First(x => x.IsApplicableFor(_arg));

        public IEnumerable<INodeFormat> Ignored => _formats.Where(x => x.IsApplicableFor(_arg)).Skip(1);
    }
}