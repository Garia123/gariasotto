using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeTravel.ServiceInterface;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.Service.Test
{
    [TestClass]
    public class ReviewServiceTest
    {
        private Mock<IUnitOfWork> mockUOW;

        [TestInitialize]
        public void Init()
        {
            mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }

        [TestMethod]
        public void AddOk()
        {
            var reviewId = Guid.NewGuid();
            var reserveId = Guid.NewGuid();
            var touristModel = new ReviewModelIn()
            {
                Description = "Test Description",
                Rating = 2,
                ReserveId = reviewId,
            };
            var reserve = new Reserve()
            {
                Id = reserveId,
            };
            var mockRepository = new Mock<IReviewRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.Create(It.IsAny<Review>()));
            mockRepository.Setup(r => r.ReviewExists(It.IsAny<Guid>())).Returns(false);
            mockUOW.SetupGet(u => u.ReviewRepository).Returns(mockRepository.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var service = new ReviewService(mockUOW.Object);

            service.Create(touristModel);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AddInvalidModel()
        {
            var touristId = Guid.NewGuid();
            var reviewModel = new ReviewModelIn()
            {
            };
            var mockRepository = new Mock<IReviewRepository>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.ReviewRepository).Returns(mockRepository.Object);
            mockRepository.Setup(r => r.ReviewExists(It.IsAny<Guid>())).Returns(false);
            var service = new ReviewService(mockUOW.Object);

            service.Create(reviewModel);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddRepeatedReview()
        {
            var touristId = Guid.NewGuid();
            var reviewModel = new ReviewModelIn()
            {
                Description = "Test Desc",
                ReserveId = Guid.NewGuid()
            };
            var mockRepository = new Mock<IReviewRepository>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.ReviewRepository).Returns(mockRepository.Object);
            mockRepository.Setup(r => r.ReviewExists(It.IsAny<Guid>())).Returns(true);
            var service = new ReviewService(mockUOW.Object);

            service.Create(reviewModel);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var mockRepository = new Mock<IReviewRepository>(MockBehavior.Strict);
            var reviewService = new ReviewService(mockUOW.Object);
            
            reviewService.Create(null);
        }
        
        [TestMethod]
        public void GetReviews()
        {
            var lodgingId = Guid.NewGuid();
            Review[] reviews = 
            {
                new Review()
                {
                    Id = Guid.NewGuid(),
                    ReserveId = Guid.NewGuid(),
                    Rating = 2,
                    Reserve = new Reserve()
                    {
                        ContactFirstName = "Test Name",
                        ContactLastName = "Test Last Name"
                    }
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    ReserveId = Guid.NewGuid(),
                    Rating = 5,
                    Reserve = new Reserve()
                    {
                        ContactFirstName = "Test Name",
                        ContactLastName = "Test Last Name"
                    }
                }
            };
            var repoMock = new Mock<IReviewRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.GetReviewsByLodgingId(It.IsAny<Guid>())).Returns(reviews);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.ReviewRepository).Returns(repoMock.Object);
            var service = new ReviewService(mockUOW.Object);

            var result = service.GetReviews(lodgingId).ToArray();

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsNotNull(result);
            for(int i=0;i< reviews.Length; i++)
            {
                reviews[i].Id = result[i].Id;
            }
        }
    }
}