using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Models;

namespace CustomerPlatform.Data.Abstract
{
    public interface ICustomersDbClient
    {
        Task<List<CustomerDtoBase>> GetCustomers();
        Task<CustomerDtoBase> AddCustomer(CustomerDtoBase customer);
    }
}
