using System;
using WeTravel.Domain.Exceptions;
using WeTravel.Domain.Interface;

namespace WeTravel.Domain
{
    public class Review : IEntity
    {
        public Guid Id { get; set; }
        public Reserve Reserve { get; set; }
        public Guid ReserveId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        
        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is Review review)
            {
                result = Id == review.Id;
            }

            return result;
        }
        
        public override int GetHashCode()
        {
            int hash = 19;
            hash = hash * 23 + ((Id == Guid.Empty) ? 0 : Id.GetHashCode());
            return hash;
        }

        public void ValidateEntity()
        {
            ValidateId();
            ValidateReserveId();
            ValidateRating();
            ValidateDescription();
        }

        private void ValidateId()
        {
            if (Id == Guid.Empty)
            {
                throw new FormatExceptionBeautifier("Id");
            };
        }

        private void ValidateReserveId()
        {
            if (ReserveId == Guid.Empty)
            {
                throw new FormatExceptionBeautifier("Reserve Id");
            };
        }

        private void ValidateRating()
        {
            if (Rating > 5 || Rating < 1)
            {
                throw new FormatExceptionBeautifier("Rating");
            };
        }

        private void ValidateDescription()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new FormatExceptionBeautifier("Description");
            };
        }
    }
}