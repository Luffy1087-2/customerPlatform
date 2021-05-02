using System.IO;
using System.Text;
using System.Threading.Tasks;
using CustomerPlatform.WebApi.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit;

namespace CustomerPlatform.WebApi.Test.Tools
{
    public class ModelBinderUtilityTest
    {
        private const string MrGreenJson = "{\"CustomerType\":\"MrGreen\",\"FirstName\":\"NameTest\",\"LastName\":\"SurnameTest\",\"PersonalNumber\":\"PersonalNumberTest\",\"Address\":{\"StreetName\":\"AddressTest\",\"Number\":\"NumberTest\",\"ZipCode\":\"ZipCodeTest\"}}";

        [Fact]
        public async Task GetJsonDtoString_Should_Read_Request_Body_And_Return_Json_String()
        {
            //Arrange
            ModelBindingContext modelBindingContext = CreateModelBindingContext();

            //Act
            string jsonString = await ModelBinderUtility.GetJsonDtoString(modelBindingContext);

            //Assert
            Assert.Equal(MrGreenJson, jsonString);
        }

        #region Private Members

        private static ModelBindingContext CreateModelBindingContext()
        {
            var httpContext = new DefaultHttpContext();
            var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(MrGreenJson));
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
