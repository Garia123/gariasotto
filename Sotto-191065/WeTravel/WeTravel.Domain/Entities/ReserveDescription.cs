using System;
using System.ComponentModel.DataAnnotations;
using WeTravel.Domain.Exceptions;
using WeTravel.Domain.Interface;

namespace WeTravel.Domain
{
    public class ReserveDescription : IEntity
    {
        public Guid ReserveId { get; set; }
        [Required]
        [RegularExpression(DataValidator.LongTextRegex)]
        public string Description { get; set; }
        public ReserveState State { get; set; }

        public virtual void ValidateEntity()
        {
            ValidateReserveId();
        }

        private void ValidateReserveId()
        {
            if (ReserveId == Guid.Empty)
            {
                throw new FormatExceptionBeautifier("ReserveId");
            }
        }
    }
}
