using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Data.Abstract;

namespace CustomerPlatform.Data.Repositories
{
    internal sealed class CustomersDataRepository : ICustomersDataRepository
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
