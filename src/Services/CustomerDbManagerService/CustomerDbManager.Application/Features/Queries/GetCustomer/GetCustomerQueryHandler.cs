using CustomerDbManager.Application.Interfaces.Repositories;
using CustomerDbManager.Application.Queries.GetCustomer;
using MediatR;

namespace CustomerDbManager.Application.Features.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQueryRequest, GetCustomerQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomerQueryResponse> Handle(GetCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var res = await _customerRepository.GetSingleAsync(x => x.TCKN == request.customer.TCKN);
            return new GetCustomerQueryResponse() { customer = res };
        }
    }
}
