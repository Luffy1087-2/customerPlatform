using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Data.Abstract;

namespace CustomerPlatform.Data.Providers
{
    public class CustomerDataProvider : ICustomerDataProvider
    {
        private readonly ICustomersDataRepository _repository;

        public CustomerDataProvider(ICustomersDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ICustomer>> GetAllCustomers()
        {
            return await _repository.GetAllCustomers();
        }

        public async Task<int> RegisterCustomer(ICustomer customer)
        {
            List<ICustomer> customers = await GetAllCustomers();

            customer.Id = customers.Any() ? customers.Count() : 0;

            customers.Add(customer);

            return customer.Id;
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
