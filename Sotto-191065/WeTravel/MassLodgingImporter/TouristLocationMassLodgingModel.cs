using System;
using System.Collections.Generic;

namespace MassLodgingImporter
{
    public class TouristLocationMassLodgingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid RegionId { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
}
