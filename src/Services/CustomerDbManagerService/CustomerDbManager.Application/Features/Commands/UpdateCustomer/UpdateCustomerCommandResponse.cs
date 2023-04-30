using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Features.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandResponse
    {
        public Customer customer { get; set; }
    }
}
