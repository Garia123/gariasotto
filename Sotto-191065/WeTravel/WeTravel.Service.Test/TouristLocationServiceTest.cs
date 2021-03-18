using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WeTravel.Service;
using WeTravel.ServiceInterface;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Entities;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.Service.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TouristLocationServiceTest
    {
        private Mock<IUnitOfWork> mockUOW;

        [TestInitialize]
        public void init()
        {
            mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }

        [TestMethod]
        public void AddOk()
        {
            var touristId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var regionId = Guid.NewGuid();
            var touristModel = new TouristLocationModelIn()
            {
                Name = "Test Name",
                Description = "Test Description",
                RegionId = regionId,
                CategoryIds = new List<Guid>()
                {
                    categoryId,
                }
            };
            var tourist = new TouristLocation()
            {
                Id = touristId,
                Name = touristModel.Name,
                Region = new Region()
                {
                    Id = regionId
                },
                Description = touristModel.Description,
                TouristLocationCategories = new List<TouristLocationCategory>() {
                    new TouristLocationCategory(){
                        CategoryId = categoryId,
                        TouristLocationId = touristId
                    }
                }
            };
            var mockRepository = new Mock<ITouristLocationRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.Create(It.IsAny<TouristLocation>()));
            var locations = new List<TouristLocation>();
            locations.Add(tourist);
            mockRepository.Setup(r => r.Get(It.IsAny<TouristLocationModelFilter>())).Returns(locations);
            mockUOW.SetupGet(u => u.TouristLocationRepository).Returns(mockRepository.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var service = new TouristLocationService(mockUOW.Object);

            service.Create(touristModel);

            IEnumerable<TouristLocationModelOut> result = service
                .GetTouristLocations(new TouristLocationModelFilter());
            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(new List<TouristLocationModelOut>(result).Exists(t => t.Id == touristId));
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var mockRepository = new Mock<ITouristLocationRepository>(MockBehavior.Strict);
            var lodgingService = new LodgingService(mockUOW.Object,null);

            lodgingService.Create(null);
        }

        [TestMethod]
        public void GetById()
        {
            var touristId = Guid.NewGuid();
            var regionId = Guid.NewGuid();
            var touristLocation = new TouristLocation()
            {
                Id = touristId,
                Name = "Test Name1",
                Region = new Region()
                {
                    Id = regionId
                },
                Description = "Test Description1",
            };
            var mockRepository = new Mock<ITouristLocationRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(touristLocation);
            mockUOW.SetupGet(u => u.TouristLocationRepository).Returns(mockRepository.Object);
            ITouristLocationService service = new TouristLocationService(mockUOW.Object);

            var result = service.GetById(touristId);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(result.Id == touristId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetByIdError()
        {
            var mockRepository = new Mock<ITouristLocationRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.GetById(It.IsAny<Guid>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            mockUOW.SetupGet(u => u.TouristLocationRepository).Returns(mockRepository.Object);
            ITouristLocationService service = new TouristLocationService(mockUOW.Object);

            var result = service.GetById(Guid.NewGuid());

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        public void GetReports()
        {
            var reserves = new List<Reserve>();
            for(int i=0; i < 20; i++)
            {
                reserves.Add(new Reserve()
                {
                    Lodging = new Lodging()
                    {
                        Name = "Lodging 1",
                        TouristLocation = new TouristLocation()
                        {
                            Name = "Location 1"
                        }
                    }
                }); ;                
            }
            for (int i = 0; i < 5; i++)
            {
                reserves.Add(new Reserve()
                {
                    Lodging = new Lodging()
                    {
                        Name = "Lodging 2",
                        TouristLocation = new TouristLocation()
                        {
                            Name = "Location 1"
                        }
                    }
                }); ;
            }
            var result = new List<ReportLineOut>() {
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
            TouristLocationReportFilter filter = new TouristLocationReportFilter()
            {
                TouristLocationName = "Location 1",
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now
            };
            var mockRepository = new Mock<IReserveRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.GetReserves(It.IsAny<TouristLocationReportFilter>())).Returns(reserves);
            mockUOW.SetupGet(u => u.ReserveRepository).Returns(mockRepository.Object);
            var service = new TouristLocationService(mockUOW.Object);

            var obtainedResult = service.GetReport(filter);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(obtainedResult.SequenceEqual(result));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void GetReportsNoName()
        {
            TouristLocationReportFilter filter = new TouristLocationReportFilter()
            {
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now
            };
            var mockRepository = new Mock<ITouristLocationRepository>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.TouristLocationRepository).Returns(mockRepository.Object);
            var service = new TouristLocationService(mockUOW.Object);

            var obtainedResult = service.GetReport(filter);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetReportsinvalidRange()
        {
            TouristLocationReportFilter filter = new TouristLocationReportFilter()
            {
                TouristLocationName = "Location 1",
                StartDate = DateTime.Now.AddDays(20),
                EndDate = DateTime.Now
            };
            var mockRepository = new Mock<ITouristLocationRepository>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.TouristLocationRepository).Returns(mockRepository.Object);
            var service = new TouristLocationService(mockUOW.Object);

            var obtainedResult = service.GetReport(filter);

            mockUOW.VerifyAll();
        }
    }
}
