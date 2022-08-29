using System;
using System.Linq;
using ObjectToTest.Constructors;
using ObjectToTest.Exceptions;

namespace ObjectToTest
{
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
                return $"{Constructor()}{new ObjectProperties(_object)}";
            }
            catch(NoConstructorException ex)
            {
                return ex.Message;
            }
        }

        private string Constructor()
        {
            foreach (var constructor in _object.Constructors())
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    return parameters.Any()
                        ? new Constructors.ParameterizedConstructor(_object, parameters).ToString()
                        : new Constructors.DefaultConstructor(_object).ToString();
                }
                catch (Exception)
                {
                    // ignore, can not create string for constructor
                }
            }

            throw new NoConstructorException(_object.GetType());
        }
    }
}
