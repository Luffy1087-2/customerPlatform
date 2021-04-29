using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;

namespace CustomerPlatform.Data.Abstract
{
    public interface ICustomerDataProvider
    {
        Task<List<CustomerDtoBase>> GetAllCustomers();
        Task<CustomerDtoBase> RegisterCustomer(CustomerDtoBase customer);
        Task UpdateCustomer(int customerId, ICustomer customer);
        Task DeleteCustomer(int customerId);
    }
}
