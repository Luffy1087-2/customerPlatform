using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.WebApi.Binders;
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
        public async Task<object> Get()
        {
            List<ICustomer> customers = await _provider.GetAllCustomers();

            return Ok(customers.Select(c => (object) c));
        }

        [HttpPost]
        public async Task<IActionResult> Register([ModelBinder(typeof(CustomerModelBinder))] CustomerDtoBase customer)
        {
            ICustomer registeredCustomer = await _provider.RegisterCustomer(customer);

            return Ok(registeredCustomer);
        }
    }
}
