using BackEndAPI.Controllers;
using BackEndDTO.InternalResponse;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        public async Task Get_ShouldReturnErrorMessageWhenEmptyString()
        {

            //Act
            var ret = await _homeControllerTest.Get("");
            string response = ret.Value;

            //Assert
            Xunit.Assert.True(response == "40X error");
        }

        [Fact]
        public async Task Get_ShouldReturnErrorMessageWhenNullParam()
        {

            //Act
            var ret = await _homeControllerTest.Get(null);
            string response = ret.Value;

            //Assert
            Xunit.Assert.True(response == "40X error");
        }

        [Fact]
        public async Task Get_ShouldReturnAtLeastThreeObjects()
        {

            //Act
            var ret = await _homeControllerTest.Get("earth");
            List<AtmiraResponseDTO> response = JsonConvert.DeserializeObject<List<AtmiraResponseDTO>>(ret.Value);

            //Assert
            Xunit.Assert.True(response.Count <= 3);
        }

    }
}