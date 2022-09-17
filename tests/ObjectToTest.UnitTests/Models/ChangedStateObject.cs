using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class ChangedStateObject
    {
        private readonly IPrice _price;
        private readonly WithUserPublicProperty _withUser;
        private IUser? _user;

        public ChangedStateObject(IPrice price, WithUserPublicProperty withUser)
        {
            _price = price;
            _withUser = withUser;
        }

        public ChangedStateObject ChangeState()
        {
            _user = _withUser.User;
            return this;
        }

        public override string ToString()
        {
            return $"{_user} with price {_price.ToDecimal()}";
        }
    }
}