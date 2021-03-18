using WeTravel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverage]
    public class TouristLocationModelOut
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid RegionId { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
}
