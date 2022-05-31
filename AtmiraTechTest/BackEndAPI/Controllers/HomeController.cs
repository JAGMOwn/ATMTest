using BackEndConstant;
using BackEndDTO;
using BackEndDTO.InternalResponse;
using BackEndHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackEndAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(string planet)
        {
            string result = planet;
            try
            {
                
                if (String.IsNullOrEmpty(planet) || string.IsNullOrWhiteSpace(planet))                    
                {
                    
                    _logger.LogError("Parámetro invalido", planet);
                    result = Constants.EmptyOrNullParamsResponseMessage.NWEPARAM;
                }
                else
                {
                    if(!Constants.Params.ALLOWEDPLANETS.Contains(planet, StringComparer.OrdinalIgnoreCase))
                    {
                        _logger.LogError("Parámetro invalido", planet);
                        result = Constants.InvalidParamsResponseMessage.FORBIDENPLANET;
                    }
                    else
                    {
                        string NasaUrl = RouteHelpers.GetNasaRoute();
                        NasaDTO nasaAsteroidsList = await HomeHelpers.GetNasaRequest(NasaUrl);
                        List<AtmiraResponseDTO> top3LargeHazardousAsteroid = HomeHelpers.GetTop3LargeHazardousAsteroids(planet, nasaAsteroidsList);
                        result = JsonConvert.SerializeObject(top3LargeHazardousAsteroid);
                    }                                                                                                                       
                }
            }

            catch (Exception exception)
            {
                _logger.LogCritical("Server exception", exception.Message);
                result = $"--> {exception.Message}";

                if (!String.IsNullOrEmpty(exception.InnerException.Message))
                {
                    _logger.LogCritical($"Internal server exception messaje {exception.InnerException.Message}", planet);
                }
            }

            return result;
        }
    }
}
