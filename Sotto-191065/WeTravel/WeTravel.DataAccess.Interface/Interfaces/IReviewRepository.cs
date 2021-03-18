using System;
using System.Collections.Generic;
using WeTravel.Domain;

namespace WeTravel.DataAccessInterface
{
    public interface IReviewRepository
    {
        public void Create(Review review);
        public IEnumerable<Review> GetReviewsByLodgingId(Guid id);
        public bool ReviewExists(Guid reserveId);
    }
}