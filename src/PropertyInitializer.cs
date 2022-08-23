using System.Linq;

namespace ObjectToTest
{
    internal class PropertyInitializer
    {
        private readonly object _object;

        public PropertyInitializer(object @object)
        {
            _object = @object;
        }

        public override string ToString()
        {
            var properties = _object.GetType().GetProperties()
                .Where(x => x.CanWrite)
                .Select(p => $"{p.Name} = {p.GetValue(_object).ToStringForInialization()}");
            var propertiesStr = string.Join(", ", properties);

            if (string.IsNullOrWhiteSpace(propertiesStr))
            {
                return string.Empty;
            }

            return "{" + propertiesStr + "}";
        }
    }
}
