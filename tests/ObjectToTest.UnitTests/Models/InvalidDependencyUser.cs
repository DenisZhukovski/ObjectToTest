using System.Threading.Tasks;
using ObjectToTest.UnitTests.Data;

namespace ObjectToTest.UnitTests.Models
{
    public class InvalidDependencyUser : IUser
    {
        private readonly IPrice _price;
        private readonly IRepository _repository;
        private readonly IRepository _incorrectArgumentsClass;

        public InvalidDependencyUser(IPrice price, IRepository repository)
        {
            _price = price;
            _repository = repository;
        }

        public Task LoginToAsync()
        {
            var result = _price.ToDecimal() + _repository.Foo();
            return Task.CompletedTask;
        }
    }
}