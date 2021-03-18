using System;

namespace WeTravel.Domain
{ 
    public class TouristLocationCategory
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid TouristLocationId { get; set; }
        public TouristLocation TouristLocation { get; set; }       
    }
}
