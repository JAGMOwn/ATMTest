using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndDTO.InternalResponse
{    
    public class AtmiraResponseDTO
    {
        public string name { get; set; }
        public double averageDiameter { get; set; }
        public string velocity { get; set; }

        public DateTime closeApproachDate { get; set; }

        public string planet { get; set; }
    }
}
