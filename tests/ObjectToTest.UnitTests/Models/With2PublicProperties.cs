using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class With2PublicProperties
    {
        public IUser User { get; set; }

        public WithUserPublicProperty UserPublicProperty { get; set; }
    }
}
