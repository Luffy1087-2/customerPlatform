using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Data.Abstract;
using MongoDB.Driver;

namespace CustomerPlatform.Data.Repositories
{
    /// <summary>
    /// This class should access to the database
    /// </summary>
    public sealed class CustomersDataRepository : ICustomersDataRepository
    {
        private readonly ICustomersDbClient _client;

        public CustomersDataRepository(ICustomersDbClient client)
        {
            _client = client;
        }

        public async Task<List<CustomerDtoBase>> GetCustomers()
        {
            return await _client.GetCustomers();
        }
    }
}
