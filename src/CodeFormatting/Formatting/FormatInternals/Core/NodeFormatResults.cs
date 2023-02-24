using System.Collections.Generic;
using System.Linq;

namespace ObjectToTest.CodeFormatting.Formatting.Core
{
    public class NodeFormatResults
    {
        private readonly List<DataAndString> _results = new();

        public bool HasFor(object item)
        {
            return _results.Any(x => ReferenceEquals(x.Data, item));
        }

        public string GetFor(object item)
        {
            return _results.First(x => ReferenceEquals(x.Data, item)).String ?? string.Empty;
        }

        public void Add(object item, string result)
        {
            _results.Add(new DataAndString
            {
                Data = item,
                String = result
            });
        }

        private sealed class DataAndString
        {
            public object? Data { get; set; }

            public string? String { get; set; }
        }
    }
}