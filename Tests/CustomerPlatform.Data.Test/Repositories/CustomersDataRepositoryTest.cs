using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Models.Customers;
using CustomerPlatform.Data.Abstract;
using CustomerPlatform.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using Xunit;

namespace CustomerPlatform.Data.Test.Repositories
{
    public class CustomersDataRepositoryTest
    {
        private readonly IMemoryCache _cache;
        private readonly ICustomersDbClient _client;
        private readonly CustomersDataRepository _sut;
        private const string CacheKey = "CustomersCacheKey";

        public CustomersDataRepositoryTest()
        {
            _cache = Substitute.For<IMemoryCache>();
            _client = Substitute.For<ICustomersDbClient>();
            _sut = new CustomersDataRepository(_cache, _client);
        }

        [Fact]
        public async Task GetCustomers_Should_Call_GetOrCreateAsync_And_Return_Customers()
        {
            //Arrange
            _client.GetCustomers().Returns(CreateCustomers());

            //Act
            List<ICustomer> customers = await _sut.GetCustomers();

            //Assert
            Assert.NotEmpty(customers);
            Assert.Equal(2, customers.Count);
            Assert.IsType<RedBetCustomerDto>(customers.ElementAt(0));
            Assert.IsType<MrGreenCustomerDto>(customers.ElementAt(1));
            await _client.Received().GetCustomers();
        }

        [Fact]
        public void EmptyCustomerCache_Should_Empty_Cache()
        {
            //Act
            _sut.EmptyCustomerCache();

            //Assert
            _cache.Received().Remove(CacheKey);
        }

        private static List<ICustomer> CreateCustomers()
        {
            return new List<ICustomer>
            {
                new RedBetCustomerDto(),
                new MrGreenCustomerDto()
            };
        }
    }
}
