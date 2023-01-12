using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class ApplicableTransformations : IEnumerable<INodeTransformation>
    {
        private readonly object _arg;
        private readonly List<INodeTransformation> _transformations;

        public ApplicableTransformations(object arg, List<INodeTransformation> transformations)
        {
            _arg = arg;
            _transformations = transformations;
        }

        public IEnumerator<INodeTransformation> GetEnumerator()
        {
            return _transformations.Where(x => x.IsApplicableFor(_arg)).ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}