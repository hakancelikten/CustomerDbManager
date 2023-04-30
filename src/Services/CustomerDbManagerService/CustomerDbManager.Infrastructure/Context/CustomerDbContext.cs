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
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext() : base() { }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
