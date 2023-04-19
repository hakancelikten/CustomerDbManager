using CustomerDbManager.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Features.Queries.VerifyCustomer
{
    public class VerifyCustomerQueryRequest : IRequest<VerifyCustomerQueryResponse>
    {
        public VerifyCustomerObject VerifyCustomerObject { get; set; }
        public VerifyCustomerQueryRequest(VerifyCustomerObject _VerifyCustomerObject)
        {
            VerifyCustomerObject = _VerifyCustomerObject;
        }
    }
}
