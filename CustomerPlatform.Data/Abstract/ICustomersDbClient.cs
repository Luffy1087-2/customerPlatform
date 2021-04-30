using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Models;

namespace CustomerPlatform.Data.Abstract
{
    internal interface ICustomersDbClient
    {
        Task<List<CustomerDtoBase>> GetCustomers();
        Task<CustomerDtoBase> AddCustomer(CustomerDtoBase customer);
    }
}
