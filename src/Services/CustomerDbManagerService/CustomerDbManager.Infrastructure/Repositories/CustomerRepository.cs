using CustomerDbManager.Application.Interfaces.Repositories;
using CustomerDbManager.Domain.Entities;

namespace CustomerDbManager.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository() : base(null)
        {

        }
    }
}
