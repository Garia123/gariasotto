using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverage]
    public class LodgingModelIn
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public IEnumerable<string> Images { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PricePerNight { get; set; }
        [Required]
        public bool Available { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string InformationText { get; set; }
        [Required]
        public Guid TouristLocationId { get; set; }
    }
}

