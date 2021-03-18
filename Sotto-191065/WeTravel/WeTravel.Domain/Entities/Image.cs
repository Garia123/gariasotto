using System;

namespace WeTravel.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public byte[] ImageData { get; set; }
    }
}
