using CustomerDbManager.Application.Interfaces.Repositories;
using CustomerDbManager.Domain.Entities;
using CustomerDbManager.Infrastructure.Context;

namespace CustomerDbManager.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
