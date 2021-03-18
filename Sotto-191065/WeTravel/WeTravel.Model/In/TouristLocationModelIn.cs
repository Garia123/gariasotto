using WeTravel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverageAttribute]
    public class TouristLocationModelIn
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public IEnumerable<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
}
