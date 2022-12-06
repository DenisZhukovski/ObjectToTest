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
        private IArguments? _sharedArguments;
        
        public ObjectAsConstructor(object @object)
        {
            _object = @object;
        }

        private IArguments SharedArguments => _sharedArguments ??= new SharedCircularProperties(
            new ObjectSharedArguments(_object)
        );
        
        public IConstructor Constructor 
        {
            get
            {
                var argument = SharedArguments.Argument(_object);
                if (argument != null)
                {
                    return new CommentLine($"Target object stored in: '{argument.Name}'");
                }
                return _object.Constructor(SharedArguments);
            }
        }
        
        public override string ToString()
        {
            try
            {
                return $"{SharedArguments}{Constructor}";
            }
            catch(NoConstructorException ex)
            {
                return ex.Message + 
                    Environment.NewLine +
                    new ObjectDependenciesTrace(
                        _object,
                        SharedArguments
                    );
            }
        }
    }
}
