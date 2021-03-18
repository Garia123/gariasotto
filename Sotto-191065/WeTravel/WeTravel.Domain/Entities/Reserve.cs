using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeTravel.Domain.Exceptions;
using WeTravel.Domain.Interface;

namespace WeTravel.Domain
{
    public class Reserve : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
        public int Price { get; set; }
        public string Telephone { get; set; }   
        public string InformationText { get; set; }
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string ContactFirstName { get; set; }
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string ContactLastName { get; set; }
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string ContactEmail { get; set; }
        public ReserveDescription ReserveDescription { get; set; }
        public Lodging Lodging { get; set; }
        public Guid LodgingId { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is Reserve reserve)
            {
                result = Id == reserve.Id;
            }

            return result;
        }
        public override int GetHashCode()
        {
            int hash = 19;
            hash = hash * 23 + ((Id == Guid.Empty) ? 0 : Id.GetHashCode());
            return hash;
        }

        public virtual void ValidateEntity()
        {
            ValidateId();
            ValidateDates();
            ValidateGuests();
            ValidateLodging();
        }

        private void ValidateId()
        {
            if (Id == Guid.Empty)
            {
                throw new FormatExceptionBeautifier("ID");
            }
        }

        private void ValidateDates()
        {
            if (CheckIn >= CheckOut)
            {
                throw new FormatExceptionBeautifier("CheckIn");
            }
        }

        private void ValidateGuests()
        {
            if (Adults <= 0 || Children < 0 || Babies < 0)
            {
                throw new FormatExceptionBeautifier("Guests");
            }
        }

        private void ValidateLodging()
        {            
            if (LodgingId == Guid.Empty)
            {
                throw new FormatExceptionBeautifier("LodgingId");
            }
        }
    }
}

