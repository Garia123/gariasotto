using System;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverageAttribute]
    public class ReserveModelIn
    {
        public Guid LodgingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
        public int Seniors { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
    }
}
