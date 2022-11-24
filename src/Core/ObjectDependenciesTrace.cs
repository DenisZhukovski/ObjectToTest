using ObjectToTest.Arguments;

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

        /**
        * @todo #60m/ARCH Need to implement object dependencies tracing logic.
         * The object should create a text report which will help the developers to understand why ToTest extension
         * method was not able to reconstruct an object entity.
         * There are 2 possible options:
         * - object does not have a public ctor
         * - one or more object's constructor arguments is invalid.
         *
         * When an object's constructor argument is invalid there are a couple of cases:
         * - counterpart was not found inside object's fields or properties. Now ToTest extension method is using
         * naming convention to find a counterpart for an argument.
         * - object argument is complex and some of its constructor arguments is invalid. In that case the report should
         * recursively generate the explanation about the argument.
         * Example:
         * ObjectCtor:
         * int a1 - valid
         * ISize size - invalid
         *  Size:
         *  int index - not found in Size class
        */
        public override string ToString()
        {
            // Implementation should be done here
            return string.Empty;
        }
    }
}