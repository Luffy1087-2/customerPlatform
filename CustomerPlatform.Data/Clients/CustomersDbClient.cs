using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Configuration;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Core.Models.Customers;
using CustomerPlatform.Data.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CustomerPlatform.Data.Clients
{
    internal sealed class CustomersDbClient : ICustomersDbClient
    {
        private readonly IMongoCollection<CustomerDtoBase> _customersCollection;

        public CustomersDbClient(IOptions<CustomersDbConfiguration> dbConfig)
        {
            BsonClassMap.RegisterClassMap<MrGreenCustomerDto>();
            BsonClassMap.RegisterClassMap<RedBetCustomerDto>();
            _customersCollection = GetCustomerAccessCollection(dbConfig);
        }

        public async Task<List<CustomerDtoBase>> GetCustomers()
        { 
            IAsyncCursor<CustomerDtoBase> customers = await _customersCollection.FindAsync(c => true);

            return customers.ToList();
        }

        public async Task<CustomerDtoBase> AddCustomer(CustomerDtoBase customer)
        {
            await _customersCollection.InsertOneAsync(customer);

            return customer;
        }

        #region Private Members

        private static IMongoCollection<CustomerDtoBase> GetCustomerAccessCollection(IOptions<CustomersDbConfiguration> dbConfig)
        {
            var client = new MongoClient(dbConfig.Value.ConnectionString);
            var database = client.GetDatabase(dbConfig.Value.DatabaseName);
            var collection = database.GetCollection<CustomerDtoBase>(dbConfig.Value.CollectionName);

            return collection;
        }

        #endregion

    }
}
