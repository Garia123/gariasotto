using System;
using System.Collections.Generic;
using System.Text;

namespace WeTravel.Domain
{
    public class ReservePriceDTO
    {
        public LodgingPriceDTO LodgingPriceDTO { get; set; }
        public int PricePerNight { get; set; }
        public int TotalDays { get; set; }
    }
}
