using System.Linq;
using System.Text;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;

namespace ObjectToTest
{
    public class ObjectDependenciesTrace
    {
        private readonly object _object;
        private readonly IArguments _sharedArguments;

        public ObjectDependenciesTrace(object @object, IArguments sharedArguments)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine();
            foreach (var ctor in _object.Constructors(_sharedArguments))
            {
                result.Append(ConstructorDetails(ctor, ""));
            }

            return result.ToString();
        }

        private string ConstructorDetails(IConstructor ctor, string intent)
        {
            var result = new StringBuilder();

            var objectType = ctor.Object != null
                    ? ctor.Object.GetType().Name
                    : "null";
            result.AppendLine($"{intent}{objectType}:");
            foreach (var argument in ctor.Arguments)
            {
                var isValid = argument.Constructor.IsValid ? "valid" : "invalid";
                var contains = ctor.Object?.Contains(argument.Name) ?? false;
                if (!contains)
                {
                    isValid = "not found in object";
                }

                var argumentType = argument.Object != null
                    ? argument.Object.GetType().Name
                    : "null";
                result.AppendLine($"{intent}  {argumentType} {argument.Name} - {isValid}");
                if (!argument.Constructor.IsValid && argument.Constructor.Arguments.Any())
                {
                    result.Append(ConstructorDetails(argument.Constructor, intent + "  "));
                }
            }

            return result.ToString();
        }
    }
}
