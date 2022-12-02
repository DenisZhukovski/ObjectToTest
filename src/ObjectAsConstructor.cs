using System;
using System.Runtime.CompilerServices;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.Exceptions;

[assembly: InternalsVisibleTo("ObjectToTest.UnitTests")]

namespace ObjectToTest
{
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
