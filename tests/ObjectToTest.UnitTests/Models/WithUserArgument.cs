using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class WithUserArgument
    {
        private readonly IUser user;
        private readonly WithUserPublicProperty withUserPublicProperty;

        public WithUserArgument(IUser user, WithUserPublicProperty withUserPublicProperty)
        {
            this.user = user;
            this.withUserPublicProperty = withUserPublicProperty;
        }
    }
}
