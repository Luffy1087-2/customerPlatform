using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ICustomer> GetAllCustomers()
        {
            return _repository.GetAllCustomers();
        }

        public int RegisterCustomer(ICustomer customer)
        {
            List<ICustomer> customers = GetAllCustomers().ToList();

            customer.Id = customers.Any() ? customers.Max(c => c.Id) : 0;

            customers.Add(customer);

            return customer.Id;
        }

        public void UpdateCustomer(int customerId, ICustomer customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
