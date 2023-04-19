using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Application.Features.Queries.VerifyCustomer;
using CustomerDbManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerDbManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CreateCustomer()
        {
            //var VerifyCustomerObject = new VerifyCustomerObject()
            //{
            //    BirthDateYear = customer.BirthDate.Year,
            //    FirstName = customer.FirstName,
            //    LastName = customer.LastName
            //};
            var res = await _mediator.Send(new VerifyCustomerQueryRequest(new VerifyCustomerObject()));

            return Ok(res);
        }
    }
}
