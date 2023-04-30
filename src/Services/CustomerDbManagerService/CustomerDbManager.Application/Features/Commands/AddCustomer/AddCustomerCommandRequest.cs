using CustomerDbManager.Domain.Entities;
using MediatR;

namespace CustomerDbManager.Application.Features.Commands.AddCustomer
{
    public class AddCustomerCommandRequest : IRequest<AddCustomerCommandResponse>
    {
        public Customer customer { get; set; }
        public AddCustomerCommandRequest(Customer customer)
        {
            this.customer = customer;
        }
    }
}
