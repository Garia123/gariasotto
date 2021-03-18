using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeTravel.Domain.Entities;
using WeTravel.Domain.Exceptions;
using WeTravel.Domain.Interface;

namespace WeTravel.Domain
{
    public class TouristLocation : IEntity
    {
        public Guid Id { get; set; }
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string Name { get; set; }
        [RegularExpression(DataValidator.LongTextRegex)]
        public string Description { get; set; }
        public Image Image { get; set; }
        public Region Region { get; set; }
        public IEnumerable<Lodging> Lodgings { get; set; } = new List<Lodging>();
        public IEnumerable<TouristLocationCategory> TouristLocationCategories { get; set; } = new List<TouristLocationCategory>();

        public virtual void ValidateEntity()
        {
            ValidateId();
        }

        private void ValidateId()
        {
            if (Id == Guid.Empty)
            {
                throw new FormatExceptionBeautifier("Id");
            }
        }
    }
}
