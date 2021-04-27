using CustomerPlatform.Binders;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return "Test";
        }

        [HttpPost]
        public IActionResult Post([ModelBinder(typeof(CustomerModelBinder))] CustomerDtoBase customer)
        {
            return Ok(customer);
        }
    }
}
