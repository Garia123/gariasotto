using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Domain.Test
{
    public class ReviewTest
    {
        [TestMethod]
        public void Valid()
        {
            var review = new Review()
            {
                Id = Guid.NewGuid(),
                ReserveId = Guid.NewGuid(),
                Description = "Test Description",
                Rating = 4,
                Reserve = new Reserve(),
            };

            review.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingId()
        {
            var review = new Review()
            {
                ReserveId = Guid.NewGuid(),
                Description = "Test Description",
                Rating = 4,
                Reserve = new Reserve(),
            };

            review.ValidateEntity();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingReserveId()
        {
            var review = new Review()
            {
                Id = Guid.NewGuid(),
                Description = "Test Description",
                Rating = 4,
                Reserve = new Reserve(),
            };

            review.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingReserveDescription()
        {
            var review = new Review()
            {
                Id = Guid.NewGuid(),
                ReserveId = Guid.NewGuid(),
                Rating = 4,
                Reserve = new Reserve(),
            };

            review.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingRating()
        {
            var review = new Review()
            {
                Id = Guid.NewGuid(),
                ReserveId = Guid.NewGuid(),
                Description = "Test Description",
                Reserve = new Reserve(),
            };

            review.ValidateEntity();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void RatingInvalid()
        {
            var review = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = 10,
                ReserveId = Guid.NewGuid(),
                Description = "Test Description",
                Reserve = new Reserve(),
            };

            review.ValidateEntity();
        }
    }
}