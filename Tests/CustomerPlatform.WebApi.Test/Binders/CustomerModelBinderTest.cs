using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models.Customers;
using CustomerPlatform.WebApi.Binders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace CustomerPlatform.WebApi.Test.Binders
{
    public class CustomerModelBinderTest
    {
        private readonly ICustomerFactory _factory;
        private readonly IModelBinder _sut;
        private const string MrGreenJson = "{\"CustomerType\":\"MrGreen\",\"FirstName\":\"NameTest\",\"LastName\":\"SurnameTest\",\"PersonalNumber\":\"PersonalNumberTest\",\"Address\":{\"StreetName\":\"AddressTest\",\"Number\":\"NumberTest\",\"ZipCode\":\"ZipCodeTest\"}}";
        private const string CustomerNotImplementedJson = "{\"CustomerType\":\"NotImplemented\",\"FirstName\":\"NameTest\",\"LastName\":\"SurnameTest\",\"PersonalNumber\":\"PersonalNumberTest\",\"Address\":{\"StreetName\":\"AddressTest\",\"Number\":\"NumberTest\",\"ZipCode\":\"ZipCodeTest\"}}";

        public CustomerModelBinderTest()
        {
            _factory = Substitute.For<ICustomerFactory>();
            _sut = new CustomerModelBinder(_factory);
        }

        [Fact]
        public async Task Model_Should_Be_Bound_Successfully()
        {
            //Arrange
            ModelBindingContext modelBindingContext = CreateModelBindingContext(MrGreenJson);
            _factory.Create("MrGreen", MrGreenJson).Returns(new MrGreenCustomerDto());

            //Act
            await _sut.BindModelAsync(modelBindingContext);

            //Assert
            Assert.NotNull(modelBindingContext.Result.Model);
            Assert.IsType<MrGreenCustomerDto>(modelBindingContext.Result.Model);
        }

        [Fact]
        public async Task ModelState_Should_Be_Written_When_CustomerType_Is_Not_Implemented_And_Factory_Raises_NotImplementedException()
        {
            //Arrange
            const string errorMessage = "CustomerTypeNotImplemented";
            ModelBindingContext modelBindingContext = CreateModelBindingContext(CustomerNotImplementedJson);
            _factory.Create("NotImplemented", CustomerNotImplementedJson).Throws(new NotImplementedException(errorMessage));

            //Act
            await _sut.BindModelAsync(modelBindingContext);

            //Assert
            Assert.Null(modelBindingContext.Result.Model);
            Assert.Equal(1, modelBindingContext.ModelState.ErrorCount);
            ModelStateEntry modelStateEntry = modelBindingContext.ModelState["CustomerType"];
            Assert.NotNull(modelStateEntry);
            ModelError modelError = Assert.Single(modelStateEntry.Errors);
            Assert.NotNull(modelError);
            Assert.Equal(errorMessage, modelError.ErrorMessage);
        }

        [Fact]
        public async Task ModelState_Should_Be_Written_When_Json_Is_Not_Well_Formatted()
        {
            //Arrange
            const string errorMessage = "JsonIsNotWellFormatted";
            const string notWellFormattedJson = "{/}";
            ModelBindingContext modelBindingContext = CreateModelBindingContext(notWellFormattedJson);

            //Act
            await _sut.BindModelAsync(modelBindingContext);

            //Assert
            Assert.Null(modelBindingContext.Result.Model);
            Assert.Equal(1, modelBindingContext.ModelState.ErrorCount);
            ModelStateEntry modelStateEntry = modelBindingContext.ModelState["Json"];
            Assert.NotNull(modelStateEntry);
            ModelError modelError = Assert.Single(modelStateEntry.Errors);
            Assert.NotNull(modelError);
            Assert.NotEmpty(errorMessage);
        }

        #region Private Members

        private static ModelBindingContext CreateModelBindingContext(string jsonString)
        {
            var httpContext = new DefaultHttpContext();
            var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(jsonString));
            httpContext.Request.Body = memoryStream;
            return new DefaultModelBindingContext
            {
                ModelState = new ModelStateDictionary(),
                ActionContext = new ActionContext
                {
                    HttpContext = httpContext
                }
            };
        }

        #endregion
    }
}
