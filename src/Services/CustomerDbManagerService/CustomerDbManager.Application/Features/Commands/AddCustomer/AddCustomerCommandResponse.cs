using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Features.Commands.AddCustomer
{
    public class AddCustomerCommandResponse
    {
        public Customer customer { get; set; }
    }
}
