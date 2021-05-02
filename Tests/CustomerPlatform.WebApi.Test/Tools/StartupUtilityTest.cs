using CustomerPlatform.WebApi.Tools;
using Xunit;

namespace CustomerPlatform.WebApi.Test.Tools
{
    public class StartupUtilityTest
    {
        [Fact]
        public void GetStartupAssemblies_Should_Return_Startup_Assemblies()
        {
            //Act
            string assemblies = StartupUtility.GetStartupAssemblies();

            //Assert
            Assert.Equal("CustomerPlatform.Core;CustomerPlatform.Data;CustomerPlatform.WebApi.Test", assemblies);
        }
    }
}
