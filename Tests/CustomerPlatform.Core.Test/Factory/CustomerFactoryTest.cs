using System;
using System.Data;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Factory;
using CustomerPlatform.Core.Models;
using CustomerPlatform.Core.Models.Base;
using CustomerPlatform.Core.Models.Customers;
using Xunit;

namespace CustomerPlatform.Core.Test.Factory
{
    public class CustomerFactoryTest
    {
        private readonly ICustomerFactory _sut;

        private const string RedBetJson = "{\"CustomerType\":\"RedBet\",\"FirstName\":\"NameTest\",\"LastName\":\"SurnameTest\",\"FavoriteFootballTeam\":\"FavoriteFootballTeamTest\",\"Address\":{\"StreetName\":\"AddressTest\",\"Number\":\"NumberTest\",\"ZipCode\":\"ZipCodeTest\"}}";
        private const string MrGreenJson = "{\"CustomerType\":\"MrGreen\",\"FirstName\":\"NameTest\",\"LastName\":\"SurnameTest\",\"PersonalNumber\":\"PersonalNumberTest\",\"Address\":{\"StreetName\":\"AddressTest\",\"Number\":\"NumberTest\",\"ZipCode\":\"ZipCodeTest\"}}";

        public CustomerFactoryTest()
        {
            _sut = new CustomerFactory();
        }

        [Fact]
        public void When_RedBet_Json_Is_Passed_With_Proper_CustomerType_It_Should_Return_RedBetCustomerDto()
        {
            //Act
            ICustomer customer = _sut.Create("RedBet", RedBetJson);

            //Assert
            RedBetCustomerDto redBetCustomerDto = Assert.IsType<RedBetCustomerDto>(customer);
            Assert.NotNull(redBetCustomerDto);
            Assert.Equal("FavoriteFootballTeamTest", redBetCustomerDto.FavoriteFootballTeam);
            AssertBaseFields(redBetCustomerDto);
        }

        [Fact]
        public void When_MrGreen_Json_Is_Passed_With_Proper_CustomerType_It_Should_Return_MrGreenCustomerDto()
        {
            //Act
            ICustomer customer = _sut.Create("MrGreen", MrGreenJson);

            //Assert
            MrGreenCustomerDto mrGreenCustomerDto = Assert.IsType<MrGreenCustomerDto>(customer);
            Assert.Equal("PersonalNumberTest", mrGreenCustomerDto.PersonalNumber);
            AssertBaseFields(mrGreenCustomerDto);
        }

        [Fact]
        public void When_Not_Implemented_CustomerType_Is_Passed_It_Should_Throw_NotImplementedException()
        {
            //Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Create("NotImplementedCustomerType", RedBetJson));
        }

        [Fact]
        public void When_Json_Cant_Be_Bound_It_Should_Throw_ConstraintException()
        {
            //Act & Assert
            Assert.Throws<ConstraintException>(() => _sut.Create("MrGreen", "{}"));
        }

        #region Private Members

        private static void AssertBaseFields(CustomerDtoBase redBetCustomerDto)
        {
            Assert.Equal("NameTest", redBetCustomerDto.FirstName);
            Assert.Equal("SurnameTest", redBetCustomerDto.LastName);
            Assert.NotNull(redBetCustomerDto.Address);
            Assert.Equal("AddressTest", redBetCustomerDto.Address.StreetName);
            Assert.Equal("NumberTest", redBetCustomerDto.Address.Number);
            Assert.Equal("ZipCodeTest", redBetCustomerDto.Address.ZipCode);
        }

        #endregion
    }
}
