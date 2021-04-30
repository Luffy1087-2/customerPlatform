using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Data.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace CustomerPlatform.Data.Repositories
{
    internal sealed class CustomersDataRepository : ICustomersDataRepository
    {
        private const string CacheKey = "CustomersCacheKey";
        private readonly IMemoryCache _cache;
        private readonly ICustomersDbClient _client;

        public CustomersDataRepository(IMemoryCache cache, ICustomersDbClient client)
        {
            _cache = cache;
            _client = client;
        }

        public async Task<List<ICustomer>> GetCustomers()
        {
            List<ICustomer> customers = await _cache.GetOrCreateAsync(CacheKey, async entry => await GetDbCustomers(entry));

            return customers;
        }

        public void EmptyCustomerCache()
        {
            _cache.Remove(CacheKey);
        }

        #region Private Members

        private async Task<List<ICustomer>> GetDbCustomers(ICacheEntry entry)
        {
            entry.SetSlidingExpiration(TimeSpan.MaxValue);

            return await _client.GetCustomers();
        }

        #endregion
    }
}
