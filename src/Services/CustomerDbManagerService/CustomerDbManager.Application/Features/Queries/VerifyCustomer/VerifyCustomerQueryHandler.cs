using CustomerDbManager.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Features.Queries.VerifyCustomer
{
    public class VerifyCustomerQueryHandler : IRequestHandler<VerifyCustomerQueryRequest, VerifyCustomerQueryResponse>
    {
        private readonly ICustomerService
            _customerService;

        public VerifyCustomerQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<VerifyCustomerQueryResponse> Handle(VerifyCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _customerService.VerifyCustomer(request.VerifyCustomerObject);

            return new VerifyCustomerQueryResponse() { VerifyCustomerObject = res };
        }
    }
}
