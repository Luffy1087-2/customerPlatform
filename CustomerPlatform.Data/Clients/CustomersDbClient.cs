using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Configuration;
using CustomerPlatform.Core.Models.Base;
using CustomerPlatform.Data.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CustomerPlatform.Data.Clients
{
    internal sealed class CustomersDbClient : ICustomersDbClient
    {
        private readonly IMongoCollection<CustomerDtoBase> _customersCollection;

        public CustomersDbClient(IOptions<CustomersDbConfiguration> dbConfig)
        {
            _customersCollection = GetCustomerAccessCollection(dbConfig);
        }

        public async Task<List<ICustomer>> GetCustomers()
        { 
            IAsyncCursor<ICustomer> customers = await _customersCollection.FindAsync(c => true);

            return customers.ToList();
        }

        public async Task<ICustomer> RegisterCustomer(CustomerDtoBase customer)
        {
            await _customersCollection.InsertOneAsync(customer);

            return customer;
        }

        public async Task<ICustomer> UpdateCustomer(CustomerDtoBase customer)
        {
            await _customersCollection.ReplaceOneAsync(c => c.Id == customer.Id, customer);

            return customer;
        }

        public async Task DeleteCustomer(string id)
        {
            await _customersCollection.DeleteOneAsync(c => c.Id == id);
        }

        #region Private Members

        private static IMongoCollection<CustomerDtoBase> GetCustomerAccessCollection(IOptions<CustomersDbConfiguration> dbConfig)
        {
            var client = new MongoClient(dbConfig.Value.ConnectionString);
            var database = client.GetDatabase(dbConfig.Value.DatabaseName);
            IMongoCollection<CustomerDtoBase> collection = database.GetCollection<CustomerDtoBase>(dbConfig.Value.CollectionName);

            return collection;
        }

        #endregion

    }
}
