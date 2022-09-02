using System;
using System.Linq;
using ObjectToTest.ConstructorParameters;
using ObjectToTest.Constructors;
using ObjectToTest.Exceptions;

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
                    $"{_object.ValidConstructor(sharedArguments)}" +
                    $"{new ObjectProperties(_object, sharedArguments)}";
            }
            catch(NoConstructorException ex)
            {
                return ex.Message;
            }
        }
    }
}
