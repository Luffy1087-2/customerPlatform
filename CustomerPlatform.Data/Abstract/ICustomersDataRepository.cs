using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Models;

namespace CustomerPlatform.Data.Abstract
{
    internal interface ICustomersDataRepository
    {
        /// <summary>
        /// It should access to the database before checking the cache
        /// </summary>
        /// <returns>IEnumerable&lt;ICustomer&gt; all of the customers</returns>
        Task<List<CustomerDtoBase>> GetCustomers();
        void EmptyCustomerCache();
    }
}
