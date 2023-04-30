using CustomerDbManager.Application.Interfaces.Repositories;
using MediatR;

namespace CustomerDbManager.Application.Features.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var res = _customerRepository.Update(request.customer);
            return new UpdateCustomerCommandResponse() { customer = res };
        }
    }
}
