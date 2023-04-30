using CustomerDbManager.Application.DTOs;
using CustomerDbManager.Application.Features.Queries.VerifyCustomer;
using CustomerDbManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CustomerDbManager.Application.Queries.GetCustomer;
using CustomerDbManager.Application.Features.Commands.AddCustomer;
using CustomerDbManager.Application.Utilities.Results;
using CustomerDbManager.Application.Constants;
using System.Net;
using CustomerDbManager.Application.Queries.GetAllCustomers;
using CustomerDbManager.Application.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;

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
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers([FromBody] CustomerRequestObject customerRequestObject)
        {
            var customer = new Customer()
            {
                TCKN = customerRequestObject.TCKN,
                FirstName = customerRequestObject.FirstName,
                LastName = customerRequestObject.LastName,
            };

            var resCustomers = await _mediator.Send(new GetAllCustomersQueryRequest(customer));

            if (resCustomers != null && resCustomers.customers != null)
            {
                var maskedCustomers = new List<CustomerResponseObject>();
                foreach (var _customer in resCustomers.customers)
                {
                    maskedCustomers.Add(new CustomerResponseObject
                    {
                        TCKN = _customer.TCKN.ToString().MaskWithStar(0, "*******"),
                        FirstName = _customer.FirstName.ToString().MaskWithStar(2, "*****"),
                        LastName = _customer.LastName.ToString().MaskWithStar(2, "*****"),
                        //BirthDate = "**/**/" + _customer.BirthDate.Value.Year.ToString(),
                    });
                }
                return Ok(new SuccessDataResult<List<CustomerResponseObject>>(maskedCustomers));
            }
            else return Ok(new ErrorResult(Messages.GetAllCustomerErrorMessage));

        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequestObject customerRequestObject)
        {
            DateTime? BirthDate = (!String.IsNullOrEmpty(customerRequestObject.BirthDate)
                    && DateTime.TryParse(customerRequestObject.BirthDate, out DateTime _time)
                    && _time != null)
                    ? _time : null;

            var verifyCustomerObject = new VerifyCustomerObject()
            {
                TCKN = customerRequestObject.TCKN,
                FirstName = customerRequestObject.FirstName,
                LastName = customerRequestObject.LastName,
                BirthDateYear = BirthDate.Value.Year
            };
            var resVerify = await _mediator.Send(new VerifyCustomerQueryRequest(verifyCustomerObject));

            if (resVerify != null && resVerify.VerifyCustomerObject.Verified)
            {
                var resGetCustomer = await _mediator.Send(new GetCustomerQueryRequest(new Customer { TCKN = customerRequestObject.TCKN }));

                if (resGetCustomer != null && resGetCustomer.customer != null)
                {
                    resGetCustomer.customer.FirstName = customerRequestObject.FirstName;
                    resGetCustomer.customer.LastName = customerRequestObject.LastName;
                    resGetCustomer.customer.BirthDate = BirthDate;
                    resGetCustomer.customer.UpdateDate = DateTime.UtcNow;

                    return Ok(new ErrorResult(Messages.CustomerAlreadyExistErrorMessage));
                }
                else
                {
                    var customer = new Customer()
                    {
                        FirstName = customerRequestObject.FirstName,
                        LastName = customerRequestObject.LastName,
                        BirthDate = BirthDate,
                        TCKN = customerRequestObject.TCKN,
                        CreateDate = DateTime.UtcNow,
                        Status = true
                    };

                    var resAddCustomer = await _mediator.Send(new AddCustomerCommandRequest(customer));

                    if (resAddCustomer != null && resAddCustomer.customer != null && resAddCustomer.customer.Id != 0)
                    {
                        return Ok(new SuccessDataResult<Customer>(resAddCustomer.customer));
                    }
                    else
                    {
                        return Ok(new ErrorResult(Messages.AddCustomerErrorMessage));
                    }

                }

            }
            else
            {
                return Ok(new ErrorResult(Messages.VerifyCustomerErrorMessage));
            }

        }
    }
}
