using System;
using System.Runtime.CompilerServices;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.Exceptions;

[assembly: InternalsVisibleTo("ObjectToTest.UnitTests")]

namespace ObjectToTest
{
    /*
     * @todo #:60m/ARCH ObjectAsConstructor class has to be structurally fixed to support logging
     * as decorator. Now when the class does not work as expected
     * its not an easy one to understand how the logic works. Ideally we should
     * be able to wrap the original object with Logged one to be able to see the whole
     * execution process. It seems it was a bad idea to use extension method for some
     * crucial pieces of code.
     */
    public class ObjectAsConstructor
    {
        private readonly object _object;
        private readonly IArguments _sharedArguments;
        
        public ObjectAsConstructor(object @object)
            : this(
                @object,
                new SharedCircularProperties(
                    new ObjectSharedArguments(@object, NeedRecursiveScan(@object))
                )
            )
        {
        }
        
        public ObjectAsConstructor(object @object, IArguments sharedArguments)
        {
            _object = @object;
            _sharedArguments = sharedArguments;
        }

        public IConstructor Constructor 
        {
            get
            {
                var argument = _sharedArguments.Argument(_object);
                return argument != null 
                    ? new CommentLine($"Target object stored in: '{argument.Name}'")
                    : _object.Constructor(_sharedArguments);
            }
        }
        
        public override string ToString()
        {
            try
            {
                return $"{_sharedArguments}{Constructor}";
            }
            catch(NoConstructorException ex)
            {
                return ex.Message + 
                    Environment.NewLine +
                    new ObjectDependenciesTrace(
                        _object,
                        _sharedArguments
                    );
            }
        }

        private static bool NeedRecursiveScan(object @object)
        {
            if (@object != null && !@object.GetType().Namespace.StartsWith("System"))
            {
                return true;
            }

            return false;
        }
    }
}
