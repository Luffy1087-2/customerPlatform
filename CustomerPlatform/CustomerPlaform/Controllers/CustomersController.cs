using System.Collections;
using System.Collections.Generic;
using CustomerPlatform.Binders;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerDataProvider _provider;

        public CustomersController(ICustomerDataProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public IEnumerable<ICustomer> Get()
        {
            return _provider.GetAllCustomers();
        }

        [HttpPost]
        public IActionResult Post([ModelBinder(typeof(CustomerModelBinder))] CustomerDtoBase customer)
        {
            _provider.RegisterCustomer(customer);

            return Ok(customer);
        }
    }
}
