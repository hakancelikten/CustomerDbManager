using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Features.Queries.GetCustomer
{
    public class GetCustomerQueryResponse
    {
        public Customer customer { get; set; }
    }
}
