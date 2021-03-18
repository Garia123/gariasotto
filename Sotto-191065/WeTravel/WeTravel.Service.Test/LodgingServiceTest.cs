using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WeTravel.Domain;
using System.Collections.Generic;
using WeTravel.ServiceInterface;
using WeTravel.DataAccessInterface;
using WeTravel.Model;
using System.Linq;
using WeTravel.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace WeTravel.Service.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LodgingServiceTest
    {
        private Mock<IUnitOfWork> mockUOW;

        [TestInitialize]
        public void init()
        {
            mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }

        [TestMethod]
        public void AddLodging()
        {
            var lodgingId = Guid.NewGuid();
            var touristId = Guid.NewGuid();
            var lodging = new LodgingModelIn()
            {
                Name = "Lodging Test",
                Stars = 2,
                Description = "This is a test description",
                Address = "test address",
                PricePerNight = 250,
                Available = true,
                Telephone = "123121",
                InformationText = "Information test Example",
                TouristLocationId = touristId
            };
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.Create(It.IsAny<Lodging>()));
            var lodgings = new List<Lodging>();
            lodgings.Add(new Lodging()
            {
                Id = lodgingId,
                Name = "Lodging Test",
                Stars = 2,
                Address = "test address",
                Description = "This is a test description",
                PricePerNight = 250,
                Available = true,
                Telephone = "123121",
                InformationText = "Information test Example",
                TouristLocation = new TouristLocation()
                {
                    Id = touristId
                },
            });
            mockRepository.Setup(r => r.Get(It.IsAny<LodgingModelFilter>())).Returns(lodgings);
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var lodgingService = new LodgingService(mockUOW.Object,null);

            lodgingService.Create(lodging);

            var result = lodgingService.Get(null);
            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(result.Where(l => l.Id == lodgingId).Any());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AddInvalid()
        {
            var lodging = new LodgingModelIn()
            {
                Name = "Lodging Test",
            };
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            var lodgingService = new LodgingService(mockUOW.Object,null);

            lodgingService.Create(lodging);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var lodgingService = new LodgingService(mockUOW.Object,null);

            lodgingService.Create(null);
        }

        [TestMethod]
        public void DeleteOk()
        {
            var lodgingId = Guid.NewGuid();
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.Delete(It.IsAny<Guid>()));
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            ILodgingService service = new LodgingService(mockUOW.Object,null);

            service.Delete(lodgingId);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void DeleteInvalid()
        {
            var lodgingId = Guid.NewGuid();
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.Delete(It.IsAny<Guid>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            ILodgingService service = new LodgingService(mockUOW.Object,null);

            service.Delete(lodgingId);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        public void ChangeStatusOk()
        {
            var lodgingId = Guid.NewGuid();
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.ChangeStatus(It.IsAny<Guid>()));
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            ILodgingService service = new LodgingService(mockUOW.Object,null);

            service.UpdateAvailable(lodgingId);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void ChangeStatusNonexistant()
        {
            var lodgingId = Guid.NewGuid();
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.ChangeStatus(It.IsAny<Guid>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            ILodgingService service = new LodgingService(mockUOW.Object,null);

            service.UpdateAvailable(lodgingId);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        public void GetById()
        {
            var lodgingId = Guid.NewGuid();
            var logding = new Lodging()
            {
                Id = lodgingId,
                Name = "Lodging Test",
                Stars = 2,
                Address = "test address",
                Description = "This is a test description",
                PricePerNight = 250,
                Available = true,
                Telephone = "123121",
                InformationText = "Information test Example",
                TouristLocation = new TouristLocation()
                {
                    Id = Guid.NewGuid()
                },
            };
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(logding);
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            ILodgingService service = new LodgingService(mockUOW.Object,null);

            var result = service.GetFromId(lodgingId);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(result.Id == lodgingId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetByIdError()
        {
            var mockRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.GetById(It.IsAny<Guid>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockRepository.Object);
            ILodgingService service = new LodgingService(mockUOW.Object,null);

            var result = service.GetFromId(Guid.NewGuid());

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        public void Get()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var lodgings = new List<Lodging>()
            {
                new Lodging()
                {
                    Id = Guid.NewGuid(),
                    Name = "Lodging Test",
                    Stars = 2,
                    Address = "test address",
                    Description = "This is a test description",
                    PricePerNight = 250,
                    Available = true,
                    Telephone = "123121",
                    InformationText = "Information test Example",
                    TouristLocation = new TouristLocation()
                    {
                        Id = Guid.NewGuid()
                    },
                }
            };
            var repoMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Get(It.IsAny<LodgingModelFilter>())).Returns(lodgings);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(repoMock.Object);
            var service = new LodgingService(mockUOW.Object,null);

            var result = service.Get(null);

            repoMock.VerifyAll();
        }

        [TestMethod]
        public void GetWithFilter()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var lodgings = new List<Lodging>()
            {
                new Lodging()
                {
                    Id = id1,
                    Name = "Lodging Test",
                    Stars = 2,
                    Address = "test address",
                    Description = "This is a test description",
                    PricePerNight = 250,
                    Available = true,
                    Telephone = "123121",
                    InformationText = "Information test Example",
                    TouristLocation = new TouristLocation()
                    {
                        Id = Guid.NewGuid()
                    },
                },
                new Lodging()
                {
                    Id = id2,
                    Name = "Lodging Test",
                    Stars = 2,
                    Address = "test address",
                    Description = "This is a test description",
                    PricePerNight = 100,
                    Available = true,
                    Telephone = "123121",
                    InformationText = "Information test Example",
                    TouristLocation = new TouristLocation()
                    {
                        Id = Guid.NewGuid()
                    },
                }
            }.ToArray();
            var filter = new LodgingModelFilter()
            {
                CheckIn = new DateTime(),
                CheckOut = new DateTime().AddDays(10),
                Adults = 2,
            };
            var expectedCosts = new List<int>()
            {
                250 * 10 * 2,
                100 * 10 * 2
            }.ToArray();
            var expectedIds = new List<Guid>()
            {
                id1,
                id2
            }.ToArray();
            var repoMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Get(It.IsAny<LodgingModelFilter>())).Returns(lodgings);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(repoMock.Object);
            var mockReserve = new Mock<ReservePrice>(MockBehavior.Strict);
            mockReserve.Setup(m => m.CalculateTotalForAdults(It.IsAny<ReservePriceDTO>()))
                .Returns<ReservePriceDTO>((dto) => { return dto.PricePerNight == 100 ? 100 * 10 * 2 : 250 * 10 * 2; });
            mockReserve.Setup(m => m.CalculateTotalForBabies(It.IsAny<ReservePriceDTO>()))
                .Returns<ReservePriceDTO>((dto) => { return 0; });
            mockReserve.Setup(m => m.CalculateTotalForChildren(It.IsAny<ReservePriceDTO>()))
                .Returns<ReservePriceDTO>((dto) => { return 0; });
            mockReserve.Setup(m => m.CalculateTotalForSeniors(It.IsAny<ReservePriceDTO>()))
                .Returns<ReservePriceDTO>((dto) => { return 0; });
            var service = new LodgingService(mockUOW.Object, mockReserve.Object);

            var result = service.Get(filter).ToArray();

            repoMock.VerifyAll();
            for (int i = 0; i < 2; i++)
            {
                Assert.IsTrue(result[i].Id == expectedIds[i] && result[i].TotalPrice == expectedCosts[i]);
            }
        }
    }
}

