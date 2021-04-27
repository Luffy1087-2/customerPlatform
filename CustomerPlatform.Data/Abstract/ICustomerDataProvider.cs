using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;

namespace CustomerPlatform.Data.Abstract
{
    public interface ICustomerDataProvider
    {
        Task<List<ICustomer>> GetAllCustomers();
        Task<int> RegisterCustomer(ICustomer customer);
        Task UpdateCustomer(int customerId, ICustomer customer);
        Task DeleteCustomer(int customerId);
    }
}
