using BackEndDTO;
using BackEndDTO.InternalResponse;
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
                //¿Controlar que planeta sea la tierra (la web solo devuelve los de la tierra)? TODO
                if (String.IsNullOrEmpty(planet) || string.IsNullOrWhiteSpace(planet))
                {
                    //Crear constantes con los valores de los mensajes a devolver
                    _logger.LogError("Parámetro invalido", planet);
                    result = "40X error";
                }
                else
                {
                    string startDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Now.ToString("yyyy-MM-dd");

                    string URL = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    NasaDTO rawData = JsonConvert.DeserializeObject<NasaDTO>(responseBody);
                   
                    
                    List<AsteroidsData> candidateAsteroids = rawData.near_earth_objects.Values.SelectMany(sm => sm)
                        .Where(w => w.is_potentially_hazardous_asteroid == true
                                 && w.close_approach_data.ElementAt(0).orbiting_body.Equals(planet, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    List<AtmiraResponseDTO> listToReturn;
                    if (candidateAsteroids.Count >= 3)
                    {
                        listToReturn = candidateAsteroids.OrderByDescending(obd => (obd.estimated_diameter.meters.estimated_diameter_max 
                                                                                  + obd.estimated_diameter.meters.estimated_diameter_min) / 2)
                            .Take(3)
                            .GroupBy(gb => gb.neo_reference_id)
                            .Select(s => s.FirstOrDefault().MapToAtmiraResponseDTO())
                            .ToList();
                    }
                    else
                    {
                        listToReturn = candidateAsteroids.GroupBy(gb => gb.neo_reference_id)
                            .Select(s => s.FirstOrDefault().MapToAtmiraResponseDTO())
                            .ToList();
                    }
                    result = JsonConvert.SerializeObject(listToReturn);
                }
            }

            catch (Exception exception)
            {
                _logger.LogCritical("Excepcion lanzada por el servidor", exception.Message);
                result = $"40X error --> {exception.Message}";

                if (!String.IsNullOrEmpty(exception.InnerException.Message))
                {
                    _logger.LogCritical($"Internal exception messaje {exception.InnerException.Message}", planet);
                }
            }

            return result;
        }
    }
}
