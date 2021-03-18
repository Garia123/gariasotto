using System;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverage]
    public class ReserveModelOut
    {
        public Guid Id { get; set; }
        public string InformationText { get; set; }
        public string Telephone { get; set; }       
    }
}
