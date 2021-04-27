using System.Collections.Generic;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Data.Abstract;

namespace CustomerPlatform.Data.Repositories
{
    /// <summary>
    /// This class should access to the database
    /// </summary>
    public sealed class CustomersDataRepository : ICustomersDataRepository
    {
        private readonly IEnumerable<ICustomer> _customers;

        public CustomersDataRepository()
        {
            _customers = new List<ICustomer>();
        }

        public IEnumerable<ICustomer> GetAllCustomers()
        {
            return _customers;
        }
    }
}
