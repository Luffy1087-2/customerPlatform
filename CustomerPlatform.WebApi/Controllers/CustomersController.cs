using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Core.Models.Base;
using CustomerPlatform.Core.Models.Responses;
using CustomerPlatform.WebApi.Binders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerDataProvider _provider;

        public CustomersController(ICustomerDataProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ICustomer> customers = await _provider.GetAllCustomers();

            return Ok(customers.Select(c => (object) c));
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                ICustomer customerById = await _provider.GetCustomerById(id);

                return Ok(customerById);
            }
            catch (NullReferenceException e)
            {
                return NotFound(new ErrorResponseDto(StatusCodes.Status404NotFound, e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([ModelBinder(typeof(CustomerModelBinder))] CustomerDtoBase customer)
        {
            ICustomer registeredCustomer = await _provider.RegisterCustomer(customer);

            return Ok(registeredCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _provider.DeleteCustomer(id);

                return Ok(new OkResponseDto($"Customer With Id ${id} Was Deleted"));
            }
            catch (NullReferenceException e)
            {
                return NotFound(new ErrorResponseDto(StatusCodes.Status404NotFound, e.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([ModelBinder(typeof(CustomerModelBinder))] CustomerDtoBase customer)
        {
            try
            {
                ICustomer updatedCustomer = await _provider.UpdateCustomer(customer);

                return Ok(updatedCustomer);
            }
            catch (NullReferenceException e)
            {
                return NotFound(new ErrorResponseDto(StatusCodes.Status404NotFound, e.Message));
            }
        }
    }
}
