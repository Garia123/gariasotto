using System;
using System.Collections.Generic;
using System.Text;

namespace WeTravel.Model
{
    public class ReportLineOut
    {
        public string LodgingName { get; set; }
        public int ReserveQuantities { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is ReportLineOut report)
            {
                result = report.LodgingName.Equals(LodgingName) && report.ReserveQuantities == ReserveQuantities;
            }

            return result;
        }

        public override int GetHashCode()
        {
            int hash = 19;
            hash = hash * 23 + LodgingName.GetHashCode();
            return hash;
        }
    }
}
