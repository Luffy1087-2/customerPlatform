using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Core.Models.Base;
using CustomerPlatform.Core.Models.Customers;
using CustomerPlatform.Core.Models.Responses;
using CustomerPlatform.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace CustomerPlatform.WebApi.Test.Controllers
{
    public class CustomersControllerTest
    {
        private readonly ICustomerDataProvider _provider;
        private readonly CustomersController _sut;
        private const string RedBetId = "RedBetId";
        private const string MrGreenId = "MrGreenId";
        private const string NotFoundId = "notFoundId";
        private const string ExceptionMessage = "Id was not found";

        public CustomersControllerTest()
        {
            _provider = Substitute.For<ICustomerDataProvider>();
            _sut = new CustomersController(_provider);
        }

        [Fact]
        public async Task Get_Route_Should_Return_Customers()
        {
            //Arrange
            List<ICustomer> customers = GetCustomers();
            _provider.GetAllCustomers().Returns(customers);

            //Act
            IActionResult result = await _sut.Get();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var value = okObjectResult.Value as IEnumerable<object>;
            Assert.NotNull(value);
            List<object> customerList = value.ToList();
            var redBetCustomerDto = Assert.IsType<RedBetCustomerDto>(customerList.ElementAt(0));
            Assert.Equal(RedBetId, redBetCustomerDto.Id);
            var mrGreenCustomerDto = Assert.IsType<MrGreenCustomerDto>(customerList.ElementAt(1));
            Assert.Equal(MrGreenId, mrGreenCustomerDto.Id);
        }

        [Fact]
        public async Task GetById_Route_Should_Return_Customer()
        {
            //Arrange
            List<ICustomer> customers = GetCustomers();
            _provider.GetCustomerById(RedBetId).Returns(customers.ElementAt(0));

            //Act
            IActionResult result = await _sut.GetById(RedBetId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var redBetCustomerDto = okObjectResult.Value as RedBetCustomerDto;
            Assert.NotNull(redBetCustomerDto);
            Assert.Equal(RedBetId, redBetCustomerDto.Id);
        }

        [Fact]
        public async Task GetById_Return_NotFoundObjectResult_When_Id_Is_Not_Found()
        {
            //Arrange
            _provider.GetCustomerById(NotFoundId).Throws(new NullReferenceException(ExceptionMessage));

            //Act
            IActionResult result = await _sut.GetById(NotFoundId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var errorDto = notFoundObjectResult.Value as ErrorResponseDto;
            Assert.NotNull(errorDto);
            Assert.Equal(ExceptionMessage, errorDto.ErrorMessage);
            Assert.Equal(StatusCodes.Status404NotFound, errorDto.StatusCode);
        }

        [Fact]
        public async Task Register_Route_Should_Register_The_Customer_And_Return_It()
        {
            //Arrange
            List<ICustomer> customers = GetCustomers();
            var customerToRegister = (CustomerDtoBase)customers.ElementAt(0);
            _provider.RegisterCustomer(customerToRegister).Returns(customerToRegister);

            //Act
            IActionResult result = await _sut.Register(customerToRegister);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var redBetCustomerDto = okObjectResult.Value as RedBetCustomerDto;
            Assert.Equal(customerToRegister, redBetCustomerDto);
        }

        [Fact]
        public async Task Delete_Route_Should_Delete_The_Customer()
        {
            //Act
            IActionResult result = await _sut.Delete(RedBetId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var okResponseDto = okObjectResult.Value as OkResponseDto;
            Assert.NotNull(okResponseDto);
            Assert.Equal($"Customer With Id ${RedBetId} Was Deleted", okResponseDto.Message);
            Assert.Equal(StatusCodes.Status200OK, okResponseDto.StatusCode);
        }

        [Fact]
        public async Task Delete_Return_NotFoundObjectResult_When_Id_Is_Not_Found()
        {
            //Arrange
            _provider.DeleteCustomer(NotFoundId).Throws(new NullReferenceException(ExceptionMessage));

            //Act
            IActionResult result = await _sut.Delete(NotFoundId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var errorDto = notFoundObjectResult.Value as ErrorResponseDto;
            Assert.NotNull(errorDto);
            Assert.Equal(ExceptionMessage, errorDto.ErrorMessage);
            Assert.Equal(StatusCodes.Status404NotFound, errorDto.StatusCode);
        }

        [Fact]
        public async Task Update_Route_Should_Update_The_Customer_And_Return_It()
        {
            //Arrange
            List<ICustomer> customers = GetCustomers();
            CustomerDtoBase customerToUpdate = (CustomerDtoBase)customers.ElementAt(0);
            _provider.UpdateCustomer(customerToUpdate).Returns(customerToUpdate);

            //Act

            IActionResult result = await _sut.Update(customerToUpdate);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var redBetCustomerDto = okObjectResult.Value as RedBetCustomerDto;
            Assert.NotNull(redBetCustomerDto);
            Assert.Equal(RedBetId, redBetCustomerDto.Id);
        }

        [Fact]
        public async Task Update_Return_NotFoundObjectResult_When_Id_Is_Not_Found()
        {
            //Arrange
            CustomerDtoBase customerToUpdate = new CustomerDtoBase {Id = NotFoundId};
            _provider.UpdateCustomer(customerToUpdate).Throws(new NullReferenceException(ExceptionMessage));

            //Act
            IActionResult result = await _sut.Update(customerToUpdate);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            var errorDto = notFoundObjectResult.Value as ErrorResponseDto;
            Assert.NotNull(errorDto);
            Assert.Equal(ExceptionMessage, errorDto.ErrorMessage);
            Assert.Equal(StatusCodes.Status404NotFound, errorDto.StatusCode);
        }

        #region Private Members

        private static List<ICustomer> GetCustomers()
        {
            return new List<ICustomer>
            {
                new RedBetCustomerDto{ Id = RedBetId },
                new MrGreenCustomerDto{ Id = MrGreenId }
            };
        }

        #endregion
    }
}
