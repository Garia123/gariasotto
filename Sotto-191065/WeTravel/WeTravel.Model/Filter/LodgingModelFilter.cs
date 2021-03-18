using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverageAttribute]
    public class LodgingModelFilter
    {
        public Guid TouristLocationId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
    }
}
