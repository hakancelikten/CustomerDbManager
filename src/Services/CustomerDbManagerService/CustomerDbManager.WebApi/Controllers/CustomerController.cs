using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Application.Features.Queries.VerifyCustomer;
using CustomerDbManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerObject createCustomerObject)
        {
            var verifyCustomerObject = new VerifyCustomerObject()
            {
                TCKN = createCustomerObject.TCKN,
                FirstName = createCustomerObject.FirstName,
                LastName = createCustomerObject.LastName
            };

            verifyCustomerObject.BirthDateYear =
                    (!String.IsNullOrEmpty(createCustomerObject.BirthDate)
                    && DateTime.TryParse(createCustomerObject.BirthDate, out DateTime _time)
                    && _time != null)
                    ? _time.Year : 1;

            var res = await _mediator.Send(new VerifyCustomerQueryRequest(verifyCustomerObject));
            return Ok(res);
        }
    }
}
