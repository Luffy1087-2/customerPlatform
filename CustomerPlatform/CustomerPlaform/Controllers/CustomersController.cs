using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<object> Get()
        {
            List<ICustomer> customers = await _provider.GetAllCustomers();

            return customers.Select(c => (object) c);
        }

        [HttpPost]
        public async Task<IActionResult> Post([ModelBinder(typeof(CustomerModelBinder))] CustomerDtoBase customer)
        {
            await _provider.RegisterCustomer(customer);

            return Ok(await Task.FromResult(customer));
        }
    }
}
