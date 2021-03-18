using System;
using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.ServiceInterface;

namespace WeTravel.Service
{
    public class ReviewService : ServiceBase, IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public void Create(ReviewModelIn reviewModel)
        {
            if (reviewModel == null)
            {
                throw new ArgumentExceptionBeautifier("Empty");
            }else if (UnitOfWork.ReviewRepository.ReviewExists(reviewModel.ReserveId))
            {
                throw new InvalidOperationExceptionBeautifier("Review already exists for reserve");
            }
            
            var review = GetDomainFromModel(reviewModel);
            review.ValidateEntity();
            UnitOfWork.ReviewRepository.Create(review);
            UnitOfWork.Save();
        }
        
        private Review GetDomainFromModel(ReviewModelIn reviewModel)
        {
            return new Review()
            {
                Id = Guid.NewGuid(),
                ReserveId = reviewModel.ReserveId,
                Reserve = new Reserve()
                {
                    Id  = reviewModel.ReserveId
                },
                Rating = reviewModel.Rating,
                Description = reviewModel.Description
            };
        }

        public IEnumerable<ReviewModelOut> GetReviews(Guid id)
        {
            return UnitOfWork.ReviewRepository.GetReviewsByLodgingId(id).Select(t => GetModelFromDomain(t)).ToList();
        }

        private ReviewModelOut GetModelFromDomain(Review review)
        {
            return new ReviewModelOut()
            {
                Id = review.Id,
                ReserveId = review.ReserveId,
                Name = review.Reserve.ContactFirstName,
                Surname = review.Reserve.ContactLastName,
                Rating = review.Rating,
                Review = review.Description
            };
        }
    }
}