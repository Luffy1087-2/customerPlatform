using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models.Customers;

namespace CustomerPlatform.Core.Factory
{
    internal sealed class CustomerFactory : ICustomerFactory
    {
        private readonly Dictionary<string, Func<string, ICustomer>> _customersDictionary = new Dictionary<string, Func<string, ICustomer>>(StringComparer.InvariantCultureIgnoreCase);

        public CustomerFactory()
        {
            _customersDictionary.Add("MrGreen", (jsonString) => JsonSerializer.Deserialize<MrGreenCustomerDto>(jsonString));
            _customersDictionary.Add("RedBet", (jsonString) => JsonSerializer.Deserialize<RedBetCustomerDto>(jsonString));
        }

        public ICustomer Create(string customerType, string jsonString)
        {
            bool hasCallback = _customersDictionary.TryGetValue(customerType, out Func<string, ICustomer> callback);

            if (!hasCallback)
            {
                throw new NotImplementedException($"{nameof(customerType)} {customerType} is not supported");
            }

            ICustomer customerModel = callback(jsonString);

            if (!IsModelBoundProperly(customerModel))
            {
                throw new ConstraintException($"Bad deserialize for {nameof(customerModel)}. \n {nameof(jsonString)} is {jsonString}");
            }

            return customerModel;
        }

        #region Private Members

        private static bool IsModelBoundProperly(ICustomer customerModel)
        {
            return customerModel != null &&
                   !string.IsNullOrWhiteSpace(customerModel.FirstName) &&
                   !string.IsNullOrWhiteSpace(customerModel.LastName) &&
                   !string.IsNullOrWhiteSpace(customerModel.CustomerType) &&
                   customerModel.Address != null &&
                   !string.IsNullOrWhiteSpace(customerModel.Address.Number) &&
                   !string.IsNullOrWhiteSpace(customerModel.Address.StreetName) &&
                   !string.IsNullOrWhiteSpace(customerModel.Address.ZipCode);
        }

        #endregion
    }
}
