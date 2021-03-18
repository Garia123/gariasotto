using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using WeTravel.Model;
using WeTravel.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using WeTravel.Domain.Exceptions;
using WeTravel.WebApi.Controllers;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LodgingControllerTest
    {
        [TestMethod]
        public void AddOK()
        {
            var model = new LodgingModelIn()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<LodgingModelIn>()));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            var obtainedResult = controller.Create(model);
            var contentResult = obtainedResult as OkObjectResult;

            mockBusinessLogic.VerifyAll();
            Assert.AreEqual(model, contentResult.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddInvalidModel()
        {
            var modelMock = new Mock<LodgingModelIn>(MockBehavior.Strict);
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<LodgingModelIn>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            controller.Create(modelMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNullModel()
        {
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<LodgingModelIn>()))
                .Throws(new ArgumentExceptionBeautifier(""));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            controller.Create(null);
        }

        [TestMethod]
        public void GetAll()
        {
            
            var lodgings = new List<LodgingModelOut>()
            {
                new LodgingModelOut()
                {
                    Name = "Lodging Test 1",
                    Description = "This is a test description for test 1"
                },
                new LodgingModelOut()
                {
                    Name = "Lodging Test 2",
                    Description = "This is a test description for test 2"
                }
            };
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(m => m.Get(It.IsAny<LodgingModelFilter>())).Returns(lodgings);
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            var result = controller.Get(new LodgingModelFilter());
            
            var lodgingsResult = result as OkObjectResult;
            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(lodgings.SequenceEqual(lodgingsResult.Value as IEnumerable<LodgingModelOut>));
        }

        [TestMethod]
        public void GetByFilter()
        {
            var touristLocationId = Guid.NewGuid();
            var lodgings = new List<LodgingModelOut>()
            {
                new LodgingModelOut()
                {
                    Name = "Lodging Test 1",
                    Description = "This is a test description for test 1",
                    TouristLocationId = touristLocationId
                },
            };
            var filters = new LodgingModelFilter()
            {
                TouristLocationId = touristLocationId
            };
            var mock = new Mock<ILodgingService>(MockBehavior.Strict);
            mock.Setup(m => m.Get(It.IsAny<LodgingModelFilter>())).Returns(lodgings);
            var controller = new LodgingController(mock.Object,null);

            var result = controller.Get(new LodgingModelFilter());
            var lodgingsResult = result as OkObjectResult;

            mock.VerifyAll();
            Assert.IsTrue(lodgings.SequenceEqual(lodgingsResult.Value as IEnumerable<LodgingModelOut>));
        }

        [TestMethod]
        public void GetReviews()
        {
            var lodgingId = Guid.NewGuid();
            var reviewModel = new ReviewModelOut()
            {
                ReserveId = Guid.NewGuid(),
                Name = "Test 1",
                Rating = 2,
                Surname = "Test",
                Review = "test review"
            };
            var reviews = new List<ReviewModelOut>()
            {
                reviewModel
            };
            var mock = new Mock<IReviewService>(MockBehavior.Strict);
            mock.Setup(m => m.GetReviews(It.IsAny<Guid>())).Returns(reviews);
            var controller = new LodgingController(null,mock.Object);

            var result = controller.GetReviews(lodgingId) as OkObjectResult;

            mock.VerifyAll();
            Assert.IsNotNull(result.Value);
        }

        [TestMethod]
        public void DeleteOk()
        {
            var lodgingId = Guid.NewGuid();
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Delete(It.IsAny<Guid>()));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            var obtainedResult = controller.Delete(lodgingId);
            var contentResult = obtainedResult as OkResult;
            
            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(contentResult.StatusCode == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void DeleteInvalid()
        {
            var lodgingId = Guid.NewGuid();
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Delete(It.IsAny<Guid>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            controller.Delete(lodgingId);
        }


        [TestMethod]
        public void ActivateExistant()
        {
            var lodgingId = Guid.NewGuid();
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.UpdateAvailable(It.IsAny<Guid>()));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            var obtainedResult = controller.UpdateAvailable(lodgingId);
            var contentResult = obtainedResult as OkResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(contentResult.StatusCode == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void ActivateNonExistant()
        {
            var lodgingId = Guid.NewGuid();
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.UpdateAvailable(It.IsAny<Guid>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            controller.UpdateAvailable(lodgingId);
        }

        [TestMethod]
        public void GetLodgingById()
        {
            var touristLocationToFind = Guid.NewGuid();
            var touristLocationModelOut = new TouristLocationModelOut()
            {
                Id = touristLocationToFind,
                Name = "Location 1",
                Description = "Description 1",
            };
            var touristLocations = new List<TouristLocationModelOut>()
            {
                touristLocationModelOut,
                touristLocationModelOut
            };
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((TouristLocationModelOut)touristLocations.ToArray().GetValue(0));
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            var obtainedResult = controller.GetFromId(touristLocationToFind);
            var contentResult = obtainedResult as OkObjectResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(touristLocationToFind == (contentResult.Value as TouristLocationModelOut).Id);
        }
    
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetLodgingByIdNonExistant()
        {
            var mockBusinessLogic = new Mock<ILodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetFromId(It.IsAny<Guid>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new LodgingController(mockBusinessLogic.Object,null);

            controller.GetFromId(Guid.NewGuid());
        }

        private LodgingModelOut getLodgingModel(Guid touristLocationId)
        {
            return new LodgingModelOut()
            {
                Name = "Lodging Test 2",
                Description = "This is a test description for test 2",
                TouristLocationId = touristLocationId
            };
        }
    }
}
