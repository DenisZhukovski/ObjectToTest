using System.Threading.Tasks;

namespace ObjectToTest.UnitTests.Data
{
    public class User : IUser
    {
        private readonly string _userName;

        public User(string userName)
        {
            _userName = userName;
        }

        public Task LoginToAsync()
        {
            return Task.CompletedTask;
        }
    }
}
