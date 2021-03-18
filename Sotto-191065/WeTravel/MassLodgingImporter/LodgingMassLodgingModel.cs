using System;
using System.Collections.Generic;

namespace MassLodgingImporter
{
    public class LodgingMassLodgingModel
    {
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> Images { get; set; }
        public string Description { get; set; }
        public int PricePerNight { get; set; }
        public bool Available { get; set; }
        public string Telephone { get; set; }
        public string InformationText { get; set; }
        public Guid TouristLocationId { get; set; }
        public TouristLocationMassLodgingModel TouristLocationModel { get; set; }
    }
}

