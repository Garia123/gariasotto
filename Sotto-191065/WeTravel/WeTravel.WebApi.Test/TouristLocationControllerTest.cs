using WeTravel.ServiceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeTravel.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeTravel.Domain.Exceptions;
using WeTravel.WebApi.Controllers;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TouristLocationControllerTest
    {
        [TestMethod]
        public void AddOK()
        {
            var model = new TouristLocationModelIn()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<TouristLocationModelIn>()));
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            var obtainedResult = controller.Create(model);
            var contentResult = obtainedResult as OkObjectResult;
            mockBusinessLogic.VerifyAll();

            Assert.AreEqual(model, contentResult.Value as TouristLocationModelIn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(b => b.Create(It.IsAny<TouristLocationModelIn>())).Throws(new ArgumentExceptionBeautifier(""));
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            controller.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AddInvalidModel()
        {
            var touristModel = new TouristLocationModelIn()
            {
            };
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<TouristLocationModelIn>()))
                .Throws(new FormatExceptionBeautifier(""));
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            controller.Create(touristModel);
        }

        [TestMethod]
        public void GetTouristLocations()
        {
            var touristLocations = new List<TouristLocationModelOut>()
            {
                new TouristLocationModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Location 1",
                    Description = "Description 1",
                },
                new TouristLocationModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Location 2",
                    Description = "Description 2",
                }
            };

            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetTouristLocations(null)).Returns(touristLocations);
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            var obtainedResult = controller.Get(null);
            var contentResult = obtainedResult as OkObjectResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(touristLocations.SequenceEqual(contentResult.Value as IEnumerable<TouristLocationModelOut>));
        }

        [TestMethod]
        public void GetFromCategoryAndRegionId()
        {
            var touristLocations = new List<TouristLocationModelOut>()
            {
                new TouristLocationModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Location 1",
                    Description = "Description 1",
                },
                new TouristLocationModelOut()
                {
                    Id = Guid.NewGuid(),
                    Name = "Location 2",
                    Description = "Description 2",
                }
            };
            var filter = new TouristLocationModelFilter()
            {
                RegionId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid()
            };
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetTouristLocations(It.IsAny<TouristLocationModelFilter>())).Returns(touristLocations);
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            var obtainedResult = controller.Get(filter);
            var contentResult = obtainedResult as OkObjectResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(touristLocations.SequenceEqual(contentResult.Value as IEnumerable<TouristLocationModelOut>));
        }

        [TestMethod]
        public void GetTouristLocationById()
        {
            var touristLocationToFind = Guid.NewGuid();
            var touristLocation = new TouristLocationModelOut()
            {
                Id = touristLocationToFind,
                Name = "Location 1",
                Description = "Description 1",
            };
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(touristLocation);
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            var obtainedResult = controller.GetFromId(touristLocationToFind);
            var contentResult = obtainedResult as OkObjectResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(touristLocationToFind == (contentResult.Value as TouristLocationModelOut).Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetTouristLocationByIdError()
        {
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetById(It.IsAny<Guid>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            controller.GetFromId(Guid.NewGuid());
        }

        [TestMethod]
        public void GetReportOk()
        {
            var reportResults = new List<ReportLineOut>() {
                new ReportLineOut()
                {
                    LodgingName = "Lodging 1",
                    ReserveQuantities = 20
                },
                new ReportLineOut()
                {
                    LodgingName = "Lodging 2",
                    ReserveQuantities = 5
                }
            };
            var filter = new TouristLocationReportFilter()
            {
                TouristLocationName = "Location 1",
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now
            };
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetReport(It.IsAny<TouristLocationReportFilter>())).Returns(reportResults);
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            var obtainedResult = controller.GetReport(filter);
            var contentResult = obtainedResult as OkObjectResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(reportResults.SequenceEqual(contentResult.Value as IEnumerable<ReportLineOut>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void GetReportInvalid()
        {
            var filter = new TouristLocationReportFilter()
            {
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now
            };
            var mockBusinessLogic = new Mock<ITouristLocationService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetReport(It.IsAny<TouristLocationReportFilter>())).Throws(new ArgumentExceptionBeautifier(""));
            var controller = new TouristLocationController(mockBusinessLogic.Object);

            var obtainedResult = controller.GetReport(filter);
        }
    }
}
