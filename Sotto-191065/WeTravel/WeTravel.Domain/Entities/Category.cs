using System;
using System.Collections.Generic;

namespace WeTravel.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public IEnumerable<TouristLocationCategory> TouristLocationCategories { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is Category category)
            {
                result = this.Id == category.Id;
            }

            return result;
        }

        public override int GetHashCode()
        {
            int hash = 19;
            hash = hash * 23 + ((Id == Guid.Empty) ? 0 : Id.GetHashCode());
            return hash;
        }
    }
}
