using System;
using System.Collections.Generic;
using System.Text;
using WeTravel.Domain;
using WeTravel.Model;

namespace WeTravel.ServiceInterface
{
    public interface IReviewService
    {
        void Create(ReviewModelIn reviewModel);
        IEnumerable<ReviewModelOut> GetReviews(Guid id);
    }
}
