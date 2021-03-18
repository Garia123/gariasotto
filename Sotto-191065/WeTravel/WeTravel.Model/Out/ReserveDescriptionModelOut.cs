using System;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverage]
    public class ReserveDescriptionModelOut
    {
        public Guid ReserveNumber { get; set; }
        public string ContactFullName { get; set; }
        public string State { get; set; }
        public string Description { get; set; }       
    }
}
