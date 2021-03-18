using System;
using System.Collections.Generic;
using System.Text;

namespace WeTravel.Model
{
    public class ReviewModelIn
    {
        public Guid ReserveId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
