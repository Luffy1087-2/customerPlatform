using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Data.Abstract;

namespace CustomerPlatform.Data.Repositories
{
    /// <summary>
    /// This class should access to the database
    /// </summary>
    public sealed class CustomersDataRepository : ICustomersDataRepository
    {
        private readonly List<ICustomer> _customers;

        public CustomersDataRepository()
        {
            _customers = new List<ICustomer>();
        }

        public Task<List<ICustomer>> GetAllCustomers()
        {
            return Task.FromResult(_customers);
        }
    }
}
