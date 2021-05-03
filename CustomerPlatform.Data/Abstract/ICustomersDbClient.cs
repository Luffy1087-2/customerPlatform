using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models.Base;

namespace CustomerPlatform.Data.Abstract
{
    internal interface ICustomersDbClient
    {
        Task<List<ICustomer>> GetCustomers();
        Task<ICustomer> RegisterCustomer(CustomerDtoBase customer);
        Task<ICustomer> UpdateCustomer(CustomerDtoBase customer);
        Task DeleteCustomer(string id);
    }
}
