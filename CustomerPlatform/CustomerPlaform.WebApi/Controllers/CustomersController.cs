using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Binders;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Core.Models.Customers;
using CustomerPlatform.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPlatform.WebApi.Controllers
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
        public async Task<object> Get()
        {
            List<CustomerDtoBase> customers = await _provider.GetAllCustomers();

            return Ok(customers.Select(c => (object) c));
        }

        [HttpPost]
        public async Task<IActionResult> Post([ModelBinder(typeof(CustomerModelBinder))] CustomerDtoBase customer)
        {
            CustomerDtoBase registeredCustomer = await _provider.RegisterCustomer(customer);

            return Ok(registeredCustomer);
        }
    }
}
