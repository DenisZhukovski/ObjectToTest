using System;
using System.Linq;

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
            var constructorStr = GetConstructor();
            var propertiesStr = new PropertyInitializer(_object).ToString();

            return $"{constructorStr}{propertiesStr}";
        }

        private string GetConstructor()
        {
            var constructors = _object.GetType().GetConstructors()
                .Where(x => x.IsPublic)
                .OrderByDescending(x => x.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    if (parameters.Any())
                    {
                        return new Constructors.ParameterizedConstructor(_object, parameters).ToString();
                    }
                    else
                    {
                        return new Constructors.DefaultConstructor(_object).ToString();
                    }
                }
                catch (Exception)
                {
                    //ignore, can not create string for constructor
                }
            }

            throw new ArgumentException($"Can not generage constructor string for type: {_object.GetType()}");
        }
    }
}
