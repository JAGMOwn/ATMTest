using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            }

            catch(Exception exception)
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
