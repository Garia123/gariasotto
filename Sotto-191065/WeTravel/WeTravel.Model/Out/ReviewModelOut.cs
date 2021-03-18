using System;
using System.Collections.Generic;
using System.Text;

namespace WeTravel.Model
{
    public class ReviewModelOut
    {
        public Guid Id { get; set; }
        public Guid ReserveId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
