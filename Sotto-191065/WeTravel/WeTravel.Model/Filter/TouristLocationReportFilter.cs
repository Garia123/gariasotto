using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WeTravel.Model
{
    [ExcludeFromCodeCoverage]
    public class TouristLocationReportFilter
    {
        public string TouristLocationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
