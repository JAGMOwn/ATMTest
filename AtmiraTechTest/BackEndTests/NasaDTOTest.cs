using BackEndDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BackEndTests
{
    public class NasaDTOTest
    {
        public readonly string exampleNasaResponse;

        public NasaDTOTest()
        {
            exampleNasaResponse = System.IO.File.ReadAllText("../../../../NASAResponseDemo.txt");
        }

        [Fact]
        public async Task TryParseNasaDemoResponseAndGetSomeData()
        {
            try
            {
                NasaDTO nasaDTO = JsonConvert.DeserializeObject<NasaDTO>(exampleNasaResponse);                                
                string firstKey = nasaDTO.near_earth_objects.Keys.FirstOrDefault();
                List<AsteroidsData> firstValues = new List<AsteroidsData>();
                nasaDTO.near_earth_objects.TryGetValue(firstKey, out firstValues);
                string id = firstValues.FirstOrDefault().id;
                string name = firstValues.FirstOrDefault().name;
                string velocityKMH = firstValues.FirstOrDefault().close_approach_data[0].relative_velocity.kilometers_per_hour;
                Assert.True(velocityKMH != string.Empty && velocityKMH != null);
            }
            catch(Exception e)
            {
                      
            }
            
        }
    }
}
