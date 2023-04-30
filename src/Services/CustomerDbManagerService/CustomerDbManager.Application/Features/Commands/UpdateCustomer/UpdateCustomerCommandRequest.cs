using CustomerDbManager.Domain.Entities;
using MediatR;

namespace CustomerDbManager.Application.Features.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandRequest : IRequest<UpdateCustomerCommandResponse>
    {
        public Customer customer { get; set; }
        public UpdateCustomerCommandRequest(Customer customer)
        {
            this.customer = customer;
        }
    }
}
