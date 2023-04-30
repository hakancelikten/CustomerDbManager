using CustomerDbManager.Application.Features.Queries.GetCustomer;
using CustomerDbManager.Domain.Entities;
using MediatR;

namespace CustomerDbManager.Application.Queries.GetCustomer
{
    public class GetCustomerQueryRequest : IRequest<GetCustomerQueryResponse>
    {
        public Customer customer { get; set; }
        public GetCustomerQueryRequest(Customer customer)
        {
            this.customer = customer;
        }
    }
}
