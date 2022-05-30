using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEndDTO.InternalResponse
{
    public static class AtmiraResponseMapperDTO
    {
        public static AtmiraResponseDTO MapToAtmiraResponseDTO(this AsteroidsData asteroidData)
        {
            return new AtmiraResponseDTO()
            {
                name = asteroidData.name,
                averageDiameter = (asteroidData.estimated_diameter.kilometers.estimated_diameter_max + asteroidData.estimated_diameter.kilometers.estimated_diameter_min) / 2,
                closeApproachDate = asteroidData.close_approach_data[0].close_approach_date,
                velocity = asteroidData.close_approach_data.ElementAt(0).relative_velocity.kilometers_per_hour,
                planet = asteroidData.close_approach_data.ElementAt(0).orbiting_body                
            };
        }
    }
}

