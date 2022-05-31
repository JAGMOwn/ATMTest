using BackEndDTO;
using BackEndDTO.InternalResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackEndHelpers
{
    public static class HomeHelpers
    {
        public static async Task<NasaDTO> GetNasaRequest(string url)
        {
            NasaDTO rawData = new NasaDTO();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                rawData = JsonConvert.DeserializeObject<NasaDTO>(responseBody);
            }
            catch (Exception e)
            {

            }
            return rawData;
        }

        public static List<AtmiraResponseDTO> GetTop3LargeHazardousAsteroids(string planet,NasaDTO nasaApiResponse)
        {
            List<AsteroidsData> candidateAsteroids = nasaApiResponse.near_earth_objects.Values.SelectMany(sm => sm)
                        .Where(w => w.is_potentially_hazardous_asteroid == true
                                 && w.close_approach_data.ElementAt(0).orbiting_body.Equals(planet, StringComparison.OrdinalIgnoreCase))
                        .ToList();
            List<AtmiraResponseDTO> listToReturn;
            if (candidateAsteroids.Count >= 3)
            {
                listToReturn = candidateAsteroids.OrderByDescending(obd => (obd.estimated_diameter.kilometers.estimated_diameter_max
                                                                          + obd.estimated_diameter.kilometers.estimated_diameter_min) / 2)
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
            return listToReturn;
        }
    }
}
