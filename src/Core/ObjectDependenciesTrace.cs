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

        public ObjectDependenciesTrace(object @object)
            : this(
                @object,
                new SharedCircularProperties(
                    new ObjectSharedArguments(@object)
                )
              )
        {
        }

        public ObjectDependenciesTrace(object @object, IArguments sharedArguments)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var ctor in _object.Constructors(_sharedArguments))
            {
                result.Append(ConstructorDetails(ctor, ""));
            }

            return result.ToString();
        }

        private string ConstructorDetails(IConstructor ctor, string intent)
        {
            var result = new StringBuilder();
            result.AppendLine($"{intent}ctor {ctor.Type()}");
            foreach (var argument in ctor.Arguments)
            {
                result.AppendLine($"{intent}  {argument.Type()} {argument.Name} - {ArgumentState(ctor, argument)}");
                if (!argument.Constructor.IsValid && argument.Constructor.Arguments.Any())
                {
                    result.Append(ConstructorDetails(argument.Constructor, intent + "  "));
                }
            }

            return result.ToString();
        }

        private static string ArgumentState(IConstructor ctor, IArgument argument)
        {
            var state = argument.Constructor.IsValid ? "valid" : "invalid";
            var contains = ctor.Object?.Contains(argument.Name) ?? false;
            if (!contains)
            {
                state = "not found in object";
            }

            return state;
        }
    }
}
