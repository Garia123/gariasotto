using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverageAttribute]
    public class TouristLocationModelFilter
    {
        public Guid RegionId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid TouristLocationId { get; set; }
    }
}
