using System;

namespace WeTravel.Domain
{
    public class LodgingPriceDTO
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
        public int Seniors { get; set; }
    }
}
