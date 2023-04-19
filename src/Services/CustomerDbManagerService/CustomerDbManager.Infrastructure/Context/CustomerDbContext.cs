using CustomerDbManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitListener.Application.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Infrastructure.Context
{
    public class CustomerDbContext : DbContext, IUnitOfWork
    {
        public CustomerDbContext() : base() { }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
