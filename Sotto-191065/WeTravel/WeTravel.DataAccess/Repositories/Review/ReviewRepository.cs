using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;

namespace WeTravel.DataAccess
{
    public class ReviewRepository : IReviewRepository
    {
        private DbContext _context;

        public ReviewRepository(DbContext context)
        {
            _context = context;
        }

        public void Create(Review review)
        {
            CheckReserve(review.ReserveId);
            _context.Set<Review>().Add(review);            
        }

        private void CheckReserve(Guid reviewReserveId)
        {
            if (!_context.Set<Domain.Reserve>().Any(r => r.Id == reviewReserveId))
            {
                throw new InvalidOperationExceptionBeautifier("review does exists");
            }
        }

        public IEnumerable<Review> GetReviewsByLodgingId(Guid id)
        {
            return _context.Set<Review>().Include(r => r.Reserve)
                .Where(r => r.Reserve.LodgingId == id).ToList();
        }

        public bool ReviewExists(Guid reserveId)
        {
            return _context.Set<Reserve>().Any(reserve => reserve.Id == reserveId);
        }
    }
}