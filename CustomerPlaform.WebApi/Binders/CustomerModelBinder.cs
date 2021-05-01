﻿using System;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.WebApi.Tools;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomerPlatform.WebApi.Binders
{
    public class CustomerModelBinder : IModelBinder
    {
        private readonly ICustomerFactory _factory;

        public CustomerModelBinder(ICustomerFactory factory)
        {
            _factory = factory;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var jsonString = await ModelBinderUtility.GetJsonDtoString(bindingContext);
            var baseModel = JsonSerializer.Deserialize<CustomerDtoBase>(jsonString);

            try
            {
                ICustomer customerModel = _factory.Create(baseModel.CustomerType, jsonString);
                bindingContext.Result = ModelBindingResult.Success(customerModel);
            }
            catch (NotImplementedException e)
            {
                bindingContext.ModelState.TryAddModelError("CustomerType", e.Message);
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}