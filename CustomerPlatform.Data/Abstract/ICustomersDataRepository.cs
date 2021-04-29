using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Models;

namespace CustomerPlatform.Data.Abstract
{
    public interface ICustomersDataRepository
    {
        /// <summary>
        /// It should access to the database
        /// </summary>
        /// <returns>IEnumerable&lt;ICustomer&gt; all of the customers</returns>
        Task<List<CustomerDtoBase>> GetCustomers();
    }
}
