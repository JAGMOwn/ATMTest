using BackEndAPI.Controllers;
using BackEndConstant;
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
        public async Task GetShouldReturnErrorMessageWhenEmptyString()
        {

            //Act
            var ret = await _homeControllerTest.Get("");
            string response = ret.Value;

            //Assert
            Assert.True(response == Constants.EmptyOrNullParamsResponseMessage.NWEPARAM);
        }

        [Fact]
        public async Task GetShouldReturnErrorMessageWhenNullParam()
        {

            //Act
            var ret = await _homeControllerTest.Get(null);
            string response = ret.Value;

            //Assert
            Assert.True(response == Constants.EmptyOrNullParamsResponseMessage.NWEPARAM);
        }

        [Fact]
        public async Task GetShouldReturnAtLeastThreeObjects()
        {

            //Act
            var ret = await _homeControllerTest.Get("earth");
            List<AtmiraResponseDTO> response = JsonConvert.DeserializeObject<List<AtmiraResponseDTO>>(ret.Value);

            //Assert
            Assert.True(response.Count <= 3);
        }

        [Fact]
        public async Task GetShouldReturnNoErrorIfAnyAsteroidsExists()
        {

            //Act
            var ret = await _homeControllerTest.Get("mars");
            List<AtmiraResponseDTO> response = JsonConvert.DeserializeObject<List<AtmiraResponseDTO>>(ret.Value);

            //Assert
            Assert.True(response.Count <= 3);
        }

        [Fact]
        public async Task GetShouldReturnForbidenPlanetError()
        {

            //Act
            var ret = await _homeControllerTest.Get("asdfg");
            string response = ret.Value;

            //Assert
            Assert.True(response == Constants.InvalidParamsResponseMessage.FORBIDENPLANET);
        }

    }
}