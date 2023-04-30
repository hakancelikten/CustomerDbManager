using CustomerDbManager.Application.Features.Queries.GetAllCustomers;
using CustomerDbManager.Application.Features.Queries.GetCustomer;
using CustomerDbManager.Domain.Entities;
using MediatR;

namespace CustomerDbManager.Application.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryRequest : IRequest<GetAllCustomersQueryResponse>
    {
        public Customer customer { get; set; }
        public GetAllCustomersQueryRequest(Customer customer)
        {
            this.customer = customer;
        }
    }
}
