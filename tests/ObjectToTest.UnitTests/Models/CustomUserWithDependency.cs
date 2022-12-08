using System.Threading.Tasks;
using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class CustomUserWithDependency : IUser 
    {
        private readonly IUser _user;

        public CustomUserWithDependency(IUser user)
        {
            _user = user;
        }
        
        public Task LoginToAsync()
        {
            return _user.LoginToAsync();
        }
    }
}