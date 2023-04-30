using CustomerDbManager.Application.Interfaces.Repositories;
using MediatR;

namespace CustomerDbManager.Application.Features.Commands.AddCustomer
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommandRequest, AddCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<AddCustomerCommandResponse> Handle(AddCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var res = await _customerRepository.AddAsync(request.customer);
            await _customerRepository.SaveEntitiesAsync(cancellationToken);
            return new AddCustomerCommandResponse() { customer = res };
        }
    }
}
