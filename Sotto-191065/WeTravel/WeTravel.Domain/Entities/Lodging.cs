using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeTravel.Domain.Entities;
using WeTravel.Domain.Exceptions;
using WeTravel.Domain.Interface;

namespace WeTravel.Domain
{
    public class Lodging : IEntity
    {
        public Guid Id { get; set; }
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string Name { get; set; }
        public int Stars { get; set; }
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string Address { get; set; }
        public IEnumerable<Image> Images { get; set; }
        [RegularExpression(DataValidator.LongTextRegex)]
        public string Description { get; set; }
        public int PricePerNight { get; set; }
        public bool Available { get; set; }
        [RegularExpression(DataValidator.TelephoneRegex)]
        public string Telephone { get; set; }
        [RegularExpression(DataValidator.LongTextRegex)]
        public string InformationText { get; set; }
        public TouristLocation TouristLocation { get; set; }
        public IEnumerable<Reserve> Reserves { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is Lodging lodging)
            {
                result = Id == lodging.Id;
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
            ValidateStars();
            ValidateTouristLocation();
            ValidatePricePerNight();
        }

        private void ValidatePricePerNight()
        {
            if (PricePerNight<1)
            {
                throw new FormatExceptionBeautifier("PRICE_PER_NIGHT");
            }
        }
        
        private void ValidateTouristLocation()
        {
            if (TouristLocation == null)
            {
                throw new FormatExceptionBeautifier("TURIST_LOCATION");
            }
        }

        private void ValidateStars()
        {
            if(Stars > 5 || Stars < 1)
            {
                throw new FormatExceptionBeautifier("STARS");
            }
        }

        private void ValidateId()
        {
            if (Id == Guid.Empty)
            {
                throw new FormatExceptionBeautifier("ID");
            }
        }
    }
}
