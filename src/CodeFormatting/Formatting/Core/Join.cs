using System;
using System.Text;

namespace ObjectToTest.CodeFormatting.Formatting
{
    public class Join
    {
        private readonly string[] _values;
        private readonly Func<ItemInfo, string> _formatEach;

        public Join(string[] values, Func<ItemInfo, string> formatEach)
        {
            _values = values;
            _formatEach = formatEach;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < _values.Length; i++)
            {
                var info = new ItemInfo()
                {
                    String = _values[i],
                    IsFirst = i == 0,
                    IsLast = i == _values.Length - 1
                };

                var result = _formatEach(info);

                stringBuilder.Append(result);
            }

            return stringBuilder.ToString();
        }

        public record ItemInfo
        {
            public bool IsFirst { get; set; }

            public bool IsLast { get; set; }

            public bool IsNotLast => !IsLast;

            public bool IsNotFirst => !IsFirst;

            public string String { get; set; }
        }
    }
}