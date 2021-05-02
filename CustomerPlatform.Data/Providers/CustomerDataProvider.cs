using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Core.Models.Base;
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
        public async Task<List<ICustomer>> GetAllCustomers()
        {
            return await _repository.GetCustomers();
        }

        public async Task<ICustomer> GetCustomerById(string id)
        {
            List<ICustomer> customers = await _repository.GetCustomers();
            ICustomer customerById = customers.Find(c => c.Id == id);

            if (customerById == null)
                throw new NullReferenceException($"The Customer with {nameof(id)} {id} was not found");

            return customerById;
        }

        public async Task<ICustomer> RegisterCustomer(CustomerDtoBase customer)
        {
            ICustomer addedCustomer = await _client.RegisterCustomer(customer);

            _repository.EmptyCustomerCache();

            return addedCustomer;
        }

        public async Task<ICustomer> UpdateCustomer(CustomerDtoBase customer)
        {
            await GetCustomerById(customer.Id);
            
            ICustomer updatedCustomer = await _client.UpdateCustomer(customer);

            _repository.EmptyCustomerCache();

            return updatedCustomer;
        }

        public async Task DeleteCustomer(string id)
        {
            ICustomer foundCustomer = await GetCustomerById(id);
            await _client.DeleteCustomer(foundCustomer.Id);
            
            _repository.EmptyCustomerCache();
        }
    }
}
