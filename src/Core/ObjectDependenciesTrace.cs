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
                    new ObjectSharedArguments(@object, true)
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
                ConstructorDetails(ctor, "", result);
            }

            return result.ToString();
        }

        private void ConstructorDetails(IConstructor ctor, string intent, StringBuilder result)
        {
            result.AppendLine($"{intent}ctor {ctor.Type()}");
            foreach (var argument in ctor.Arguments)
            {
                var state = ArgumentState(ctor, argument);
                result.AppendLine($"{intent}  {argument.Type} {argument.Name} - {state}");
                if (state == "invalid" && argument.Constructor.Arguments.Any())
                {
                    ConstructorDetails(argument.Constructor, intent + "    ", result);
                }
            }
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
