using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Data.Abstract;

namespace CustomerPlatform.Data.Providers
{
    internal sealed class CustomerDataProvider : ICustomerDataProvider
    {
        private readonly ICustomersDataRepository _repository;
        private readonly ICustomersDbClient _client;

        public CustomerDataProvider(ICustomersDbClient client, ICustomersDataRepository repository)
        {
            _client = client;
            _repository = repository;
        }

        public async Task<List<CustomerDtoBase>> GetAllCustomers()
        {
            return await _repository.GetCustomers();
        }

        public async Task<CustomerDtoBase> RegisterCustomer(CustomerDtoBase customer)
        {
            CustomerDtoBase addedCustomer = await _client.AddCustomer(customer);

            _repository.EmptyCustomerCache();

            return addedCustomer;
        }

        public Task UpdateCustomer(int customerId, ICustomer customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
