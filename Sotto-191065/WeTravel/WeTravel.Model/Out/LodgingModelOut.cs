using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverage]
    public class LodgingModelOut
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string InformationText { get; set; }
        public string Telephone { get; set; }
        public int Stars { get; set; }
        public IEnumerable<string> Images { get; set; }
        public int PricePerNight { get; set; }
        public bool Available { get; set; }
        public Guid TouristLocationId { get; set; }
        public int TotalPrice { get; set; }
    }
}

