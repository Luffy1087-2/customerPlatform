using System;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models.Base;
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
            try
            {
                ICustomer customerModel = await BindJsonToCustomerModel(bindingContext);
                bindingContext.Result = ModelBindingResult.Success(customerModel);

                return;
            }
            catch (JsonException e)
            {
                bindingContext.ModelState.TryAddModelError("Json", e.Message);
            }
            catch (NotImplementedException e)
            {
                bindingContext.ModelState.TryAddModelError("CustomerType", e.Message);
            }
            
            bindingContext.Result = ModelBindingResult.Failed();
        }

        #region Private Members

        private async Task<ICustomer> BindJsonToCustomerModel(ModelBindingContext bindingContext)
        {
            string jsonString = await ModelBinderUtility.GetJsonDtoString(bindingContext);

            var baseModel = JsonSerializer.Deserialize<CustomerDtoBase>(jsonString);

            ICustomer customerModel = _factory.Create(baseModel.CustomerType, jsonString);

            return customerModel;
        }

        #endregion
    }
}