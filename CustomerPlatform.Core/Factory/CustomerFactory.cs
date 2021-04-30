﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models.Customers;

namespace CustomerPlatform.Core.Factory
{
    public sealed class CustomerFactory : ICustomerFactory
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
                throw new NotImplementedException(nameof(hasCallback));
            }

            ICustomer model = callback(jsonString);

            return model;
        }
    }
}