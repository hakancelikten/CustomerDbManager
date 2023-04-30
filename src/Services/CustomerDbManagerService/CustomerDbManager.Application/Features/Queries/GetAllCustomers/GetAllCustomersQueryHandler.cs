using CustomerDbManager.Application.Interfaces.Repositories;
using CustomerDbManager.Application.Queries.GetAllCustomers;
using CustomerDbManager.Application.Queries.GetCustomer;
using CustomerDbManager.Domain.Entities;
using MediatR;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace CustomerDbManager.Application.Features.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQueryRequest, GetAllCustomersQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetAllCustomersQueryResponse> Handle(GetAllCustomersQueryRequest request, CancellationToken cancellationToken)
        {

            Expression<Func<Customer, bool>> exprTCKN = (x) => x.TCKN == request.customer.TCKN;
            Expression<Func<Customer, bool>> exprFirstName = (x) => x.FirstName == request.customer.FirstName;


            var body = Expression.AndAlso(exprTCKN.Body, exprFirstName.Body);
            var lambda = Expression.Lambda<Func<Customer, bool>>(body, exprTCKN.Parameters[0]);

            //var filterExpressions = new List<Expression<Func<Customer, object>>>();

            if (request.customer.TCKN != default(long))
            {

                //filter = x => compiled(x) && x.TCKN == request.customer.TCKN;
                //filterExpressions.Add(x => x.TCKN == request.customer.TCKN);
            }
            if (!string.IsNullOrEmpty(request.customer.FirstName))
            {

                //filterExpressions.Add(x => x.FirstName == request.customer.FirstName);
            }
            if (!string.IsNullOrEmpty(request.customer.LastName))
            {

                //filterExpressions.Add(x => x.LastName == request.customer.LastName);
            }
            var res = await _customerRepository.Get(lambda);

            return new GetAllCustomersQueryResponse() { customers = res };
        }
    }
}
