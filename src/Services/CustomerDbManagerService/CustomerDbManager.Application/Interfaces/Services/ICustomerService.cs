using CustomerDbManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<VerifyCustomerObject> VerifyCustomer(VerifyCustomerObject verifyCustomerObject);

    }
}
