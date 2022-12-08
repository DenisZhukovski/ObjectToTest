using System.Threading.Tasks;
using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class CustomUser : IUser
    {
        public Task LoginToAsync()
        {
            return Task.CompletedTask;
        }
    }
}