using CustomerDbManager.Application.Interfaces.Repositories;
using CustomerDbManager.Application.Interfaces.Services;
using CustomerDbManager.Application.Utilities.Security.Hashing;
using CustomerDbManager.Domain.Entities;
using CustomerDbManager.Infrastructure.Context;
using CustomerDbManager.Infrastructure.Repositories;
using CustomerDbManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerDbManager.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(opt =>
            {
                opt.UseSqlServer(configuration["CustomerDbConnectionStringOnLocal"]);
                opt.EnableSensitiveDataLogging();
            });
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>().UseSqlServer(configuration["CustomerDbConnectionStringOnLocal"]);

            using var dbContext = new CustomerDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123", out passwordHash, out passwordSalt);
            var customer = new Customer
            {
                Email = "celiktenhakan@hotmail.com",
                FirstName = "hakan",
                LastName = "çelikten",
                TCKN = 32095302790,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                BirthDate = new DateTime(1993, 10, 20),
                Status = true
            };
            var userEntity = dbContext.Entry(customer);
            userEntity.State = EntityState.Added;
            dbContext.SaveChanges();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
