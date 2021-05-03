using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models.Base;
using CustomerPlatform.Core.Models.Customers;
using CustomerPlatform.Data.Abstract;
using CustomerPlatform.Data.Providers;
using NSubstitute;
using Xunit;

namespace CustomerPlatform.Data.Test.Providers
{
    public class CustomerDataProviderTest
    {
        private readonly ICustomersDataRepository _repository;
        private readonly ICustomersDbClient _client;
        private readonly ICustomerDataProvider _sut;
        private const string RedBetCustomerId = "RedBetId";
        private const string MrGreenCustomerId = "MrGreenId";

        public CustomerDataProviderTest()
        {
            _repository = Substitute.For<ICustomersDataRepository>();
            _client = Substitute.For<ICustomersDbClient>();
            _sut = new CustomerDataProvider(_client, _repository);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Return_Customers()
        {
            //Arrange
            List<ICustomer> customers = CreateCustomers();
            _repository.GetCustomers().Returns(customers);

            //Act
            List<ICustomer> returnedCustomers = await _sut.GetAllCustomers();

            //Assert
            Assert.NotEmpty(returnedCustomers);
            Assert.Equal(2, returnedCustomers.Count);
            Assert.IsType<RedBetCustomerDto>(returnedCustomers.ElementAt(0));
            Assert.IsType<MrGreenCustomerDto>(returnedCustomers.ElementAt(1));
        }

        [Fact]
        public async Task GetCustomerById_Should_Return_RedBet_Customer()
        {
            //Arrange
            _repository.GetCustomers().Returns(CreateCustomers());

            //Act
            ICustomer customer = await _sut.GetCustomerById(RedBetCustomerId);

            //Assert
            RedBetCustomerDto redBetCustomerDto = Assert.IsType<RedBetCustomerDto>(customer);
            Assert.NotNull(redBetCustomerDto);
            Assert.Equal(RedBetCustomerId, redBetCustomerDto.Id);
        }

        [Fact]
        public async Task GetCustomerById_Should_Return_MrGreen_Customer()
        {
            //Arrange
            _repository.GetCustomers().Returns(CreateCustomers());

            //Act
            ICustomer customer = await _sut.GetCustomerById(MrGreenCustomerId);

            //Assert
            MrGreenCustomerDto mrGreenCustomerDto = Assert.IsType<MrGreenCustomerDto>(customer);
            Assert.NotNull(mrGreenCustomerDto);
            Assert.Equal(MrGreenCustomerId, mrGreenCustomerDto.Id);
        }

        [Fact]
        public async Task GetCustomerById_Should_Throws_When_Customer_Is_Not_Found()
        {
            //Arrange
            _repository.GetCustomers().Returns(CreateCustomers());

            //Act & Assert
            await Assert.ThrowsAnyAsync<NullReferenceException>(async () => await _sut.GetCustomerById("IdNotPresent"));
        }

        [Fact]
        public async Task RegisterCustomer_Should_Register_Customer_And_Empty_Cache()
        {
            //Arrange
            var customer = new RedBetCustomerDto();
            _client.RegisterCustomer(Arg.Any<CustomerDtoBase>()).Returns(customer);

            //Act
            ICustomer returnedCustomer = await _sut.StoreCustomer(customer);

            //Assert
            Assert.NotNull(returnedCustomer);
            Assert.IsType<RedBetCustomerDto>(returnedCustomer);
            _repository.Received().EmptyCustomerCache();
        }

        [Fact]
        public async Task UpdateCustomer_Should_Update_Customer_And_Empty_Cache()
        {
            //Arrange
            var customer = new RedBetCustomerDto { Id = RedBetCustomerId };
            _repository.GetCustomers().Returns(CreateCustomers());
            _client.UpdateCustomer(Arg.Any<CustomerDtoBase>()).Returns(customer);

            //Act
            ICustomer returnedCustomer = await _sut.UpdateCustomer(customer);

            //Assert
            Assert.NotNull(returnedCustomer);
            Assert.IsType<RedBetCustomerDto>(returnedCustomer);
            _repository.Received().EmptyCustomerCache();
        }

        [Fact]
        public async Task DeleteCustomer_Should_Delete_Customer_And_Empty_Cache()
        {
            //Arrange
            _repository.GetCustomers().Returns(CreateCustomers());

            //Act
            await _sut.DeleteCustomer(RedBetCustomerId);

            //Assert
            await _client.DeleteCustomer(RedBetCustomerId);
            _repository.Received().EmptyCustomerCache();
        }

        #region Private Members

        private static List<ICustomer> CreateCustomers()
        {
            return new List<ICustomer>
            {
                new RedBetCustomerDto { Id = RedBetCustomerId },
                new MrGreenCustomerDto{ Id = MrGreenCustomerId }
            };
        }

        #endregion
    }
}
