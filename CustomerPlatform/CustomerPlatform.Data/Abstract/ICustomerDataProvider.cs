using System.Collections.Generic;
using CustomerPlatform.Core.Abstract;

namespace CustomerPlatform.Data.Abstract
{
    public interface ICustomerDataProvider
    {
        IEnumerable<ICustomer> GetAllCustomers();
        int RegisterCustomer(ICustomer customer);
        void UpdateCustomer(int customerId, ICustomer customer);
        void DeleteCustomer(int customerId);
    }
}
