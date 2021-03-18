using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WeTravel.Domain.Interface;

namespace WeTravel.Domain
{
    public class Region : IEntity
    {
        public Guid Id { get; set; }
        [RegularExpression(DataValidator.ShortTextRegex)]
        public string Name { get; set; }
        public IEnumerable<TouristLocation> TouristLocations { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is Region region)
            {
                result = this.Id == region.Id;
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
        }
        
        private void ValidateId()
        {
            if (Id == Guid.Empty)
            {
                throw new FormatException();
            }
        }
    }
}
