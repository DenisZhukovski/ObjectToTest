using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class WithUserArgument
    {
        private readonly IUser? _user;
        private readonly WithUserPublicProperty _withUserPublicProperty;

        public WithUserArgument(IUser? user, WithUserPublicProperty withUserPublicProperty)
        {
            _user = user;
            _withUserPublicProperty = withUserPublicProperty;
        }
    }
}
