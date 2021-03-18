using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;

namespace WeTravel.DataAccess.Test
{
    [TestClass]
    public class ReviewRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;
        
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, null, null, null, 
                null, null, null, 
                null, new ReviewRepository(context));
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddReview()
        {
            var reserve = new Domain.Reserve()
            {
                Id = Guid.NewGuid()
            };
            var reviewId = Guid.NewGuid();
            var review = new Review()
            {
                Id = reviewId,
                Description = "Test Description",
                ReserveId = reserve.Id,
                Rating = 4
            };
            context.Reserves.AddRange(reserve);
            context.SaveChanges();

            uof.ReviewRepository.Create(review);
            context.SaveChanges();
            
            Assert.IsTrue(context.Reviews.Contains(review));
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void InvalidReserve()
        {
            var reserve = new Domain.Reserve()
            {
                Id = Guid.NewGuid()
            };
            var reviewId = Guid.NewGuid();
            var review = new Review()
            {
                Id = reviewId,
                Description = "Test Description",
                ReserveId = reserve.Id,
                Rating = 4
            };
            context.SaveChanges();

            uof.ReviewRepository.Create(review);
        }

        [TestMethod]
        public void GetReviewsById()
        {
            var lodgingId = Guid.NewGuid();
            var reserve = new Domain.Reserve()
            {
                Id = Guid.NewGuid(),
                LodgingId = lodgingId
            };
            var reviewId = Guid.NewGuid();
            var review = new Review()
            {
                Id = reviewId,
                Description = "Test Description",
                ReserveId = reserve.Id,
                Reserve = reserve,
                Rating = 4
            };
            context.Reserves.Add(reserve);
            context.SaveChanges();
            context.Reviews.Add(review);
            context.SaveChanges();

            var result = uof.ReviewRepository.GetReviewsByLodgingId(lodgingId);
            
            Assert.IsTrue(result.Contains(review));
        }
    }
}