using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Features.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryResponse
    {
        public List<Customer> customers { get; set; }
    }
}
