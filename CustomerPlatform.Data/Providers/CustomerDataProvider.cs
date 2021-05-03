using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
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
            ICustomer customerById = await FindCustomerById(id);

            if (customerById == null)
                ThrowNotFoundCustomerException(id);

            return customerById;
        }

        public async Task<ICustomer> StoreCustomer(CustomerDtoBase customer)
        {
            ICustomer addedCustomer = await _client.RegisterCustomer(customer);

            _repository.EmptyCustomerCache();

            return addedCustomer;
        }

        public async Task<ICustomer> UpdateCustomer(CustomerDtoBase customer)
        {
            if (await FindCustomerById(customer.Id) == null)
                ThrowNotFoundCustomerException(customer.Id);

            ICustomer updatedCustomer = await _client.UpdateCustomer(customer);

            _repository.EmptyCustomerCache();

            return updatedCustomer;
        }

        public async Task DeleteCustomer(string id)
        {
            if (await FindCustomerById(id) == null)
                ThrowNotFoundCustomerException(id);

            await _client.DeleteCustomer(id);
            
            _repository.EmptyCustomerCache();
        }

        #region Private Members

        private async Task<ICustomer> FindCustomerById(string id)
        {
            List<ICustomer> customers = await _repository.GetCustomers();

            ICustomer customerById = customers.Find(c => c.Id == id);

            return customerById;
        }

        private static void ThrowNotFoundCustomerException(string id)
        {
            throw new NullReferenceException($"The Customer with {nameof(id)} {id} was not found");
        }

        #endregion
    }
}
