using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;

namespace CustomerPlatform.Data.Abstract
{
    internal interface ICustomersDbClient
    {
        Task<List<ICustomer>> GetCustomers();
        Task<ICustomer> AddCustomer(CustomerDtoBase customer);
    }
}
