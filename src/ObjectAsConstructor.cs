using System;
using System.Linq;
using System.Runtime.CompilerServices;
using ObjectToTest.Arguments;
using ObjectToTest.Constructors;
using ObjectToTest.Exceptions;

[assembly: InternalsVisibleTo("ObjectToTest.UnitTests")]

namespace ObjectToTest
{
    /**
    * @todo #60m/ARCH Need to create Interface/Class diagram that describes the solution on high level
    * Ideally this diagram should be put into Docs folder of the repository and referenced in Readme file.
    */
    public class ObjectAsConstructor
    {
        private readonly object _object;

        public ObjectAsConstructor(object @object)
        {
            _object = @object;
        }

        public override string ToString()
        {
            try
            {
                IArguments sharedArguments = new ObjectSharedArguments(_object);
                return $"{sharedArguments}" +
                    $"{_object.ValidConstructor(sharedArguments)}";
            }
            catch(NoConstructorException ex)
            {
                return ex.Message;
            }
        }
    }
}
