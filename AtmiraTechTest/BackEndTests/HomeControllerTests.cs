using BackEndAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTests
{
    public class HomeControllerTests
    {
        private readonly HomeController _homeControllerTest;
        private readonly Mock<ILogger<HomeController>> _mockLoggerObject = new Mock<ILogger<HomeController>>();
        
        public HomeControllerTests()
        {
            _homeControllerTest = new HomeController(_mockLoggerObject.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnEmptyString()
        {

            //Act
            var ret = await _homeControllerTest.Get("someStringFirstTest");
            string response = ret.Value;

            //Assert
            Xunit.Assert.True(response == string.Empty);
        }
        
    }
}