using System;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverageAttribute]
    public class ReserveDescriptionModelIn
    {
        public Guid ReserveId { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
    }
}
