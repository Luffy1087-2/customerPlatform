using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Models.Base;

namespace CustomerPlatform.Core.Abstract
{
    public interface ICustomerDataProvider
    {
        /// <summary>
        /// Returns all customers
        /// </summary>
        /// <returns>The customers registered in the database</returns>
        Task<List<ICustomer>> GetAllCustomers();
        /// <summary>
        /// Returns the customer filtered by id
        /// </summary>
        /// <param name="id">the customer Id</param>
        /// <returns>The filtered customer</returns>
        Task<ICustomer> GetCustomerById(string id);
        /// <summary>
        /// Registers the customer into the database
        /// </summary>
        /// <param name="customer">The customer object</param>
        /// <returns>The customer that was just added</returns>
        Task<ICustomer> StoreCustomer(CustomerDtoBase customer);
        /// <summary>
        /// Updates the customer by id
        /// </summary>
        /// <param name="customer">the customer object</param>
        /// <returns>The customer that was just updated</returns>
        Task<ICustomer> UpdateCustomer(CustomerDtoBase customer);
        /// <summary>
        /// Deletes the customer by id
        /// </summary>
        /// <param name="id">the customer id</param>
        Task DeleteCustomer(string id);
    }
}
