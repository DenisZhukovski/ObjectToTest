using System;
using ObjectToTest.Arguments;

namespace ObjectToTest
{
    /// <summary>
    /// The idea is to detect the set of objects that used more that one time in different target object arguments.
    /// It should allow to reuse the argument injecting it into other object's constructors or property setters.
    /// </summary>
    public class ObjectSharedArguments : IArguments
    {
        private readonly object _object;

        public ObjectSharedArguments(object @object)
        {
            _object = @object;
        }

        public IArgument? Argument(object argument)
        {
            /**
            * @todo #12:60m/DEV The method should try to find the same argument inside
            * the target object and check if its the shared one. It means the argument is used more than 1 time inside 
            * the target object fileds or properties:
            * The example below shows sych case when user object is the shared one.
            * var user = new User("user name");
            * var withUser = new WithUserArgument(
            *   user,
            *   new WithUserPublicProperty
            *   {
            *       User = user
            *    }
            * );
            * The Test: ObjectSharedArgumentsTests.SharedArgument should be green
            */
            return null;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}

