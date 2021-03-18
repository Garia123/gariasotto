using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WeTravel.ServiceInterface;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.Service.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReserveServiceTest
    {
        private Mock<IUnitOfWork> mockUOW;

        [TestInitialize]
        public void init()
        {
            mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }

        [TestMethod]
        public void AddReserve()
        {
            var lodging = CreateLodging();
            var reserveModelIn = CreateReserveModelIn(lodging.Id);
            var reserve = CreateReserve(lodging, reserveModelIn);
            var reserveModelOut = new ReserveModelOut()
            {
                Id = reserve.Id,
                InformationText = lodging.InformationText,
                Telephone = lodging.Telephone
            };
            var mockReserveRepository = new Mock<IReserveRepository>(MockBehavior.Strict);
            var mockLodgingRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockReserveRepository.Setup(r => r.Create(It.IsAny<Reserve>()));
            mockLodgingRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(lodging);
            mockUOW.SetupGet(u => u.ReserveRepository).Returns(mockReserveRepository.Object);
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockLodgingRepository.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var mockReserve = new Mock<ReservePrice>(MockBehavior.Strict);
            mockReserve.Setup(m => m.CalculateTotalForAdults(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return dto.PricePerNight == 100 ? 100 * 10 * 2 : 250 * 10 * 2;
            });
            mockReserve.Setup(m => m.CalculateTotalForBabies(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return 0;
            });
            mockReserve.Setup(m => m.CalculateTotalForChildren(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return 0;
            });
            mockReserve.Setup(m => m.CalculateTotalForSeniors(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return 0;
            });
            var reserveService = new ReserveService(mockUOW.Object, mockReserve.Object);

            reserveService.Create(reserveModelIn);
            var result = reserveService.Create(reserveModelIn);
            mockReserveRepository.VerifyAll();
            mockUOW.VerifyAll();
    
            Assert.IsTrue(result.Telephone == reserveModelOut.Telephone);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AddInvalid()
        {
            var reserveModel = new ReserveModelIn();
            var lodging = CreateLodging();
            var mockReserveRepository = new Mock<IReserveRepository>(MockBehavior.Strict);
            var mockLodgingRepository = new Mock<ILodgingRepository>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.ReserveRepository).Returns(mockReserveRepository.Object);
            mockUOW.SetupGet(u => u.LodgingRepository).Returns(mockLodgingRepository.Object);
            mockLodgingRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(lodging);
            var mockReserve = new Mock<ReservePrice>(MockBehavior.Strict);
            mockReserve.Setup(m => m.CalculateTotalForAdults(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return dto.PricePerNight == 100 ? 100 * 10 * 2 : 250 * 10 * 2;
            });
            mockReserve.Setup(m => m.CalculateTotalForBabies(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return 0;
            });
            mockReserve.Setup(m => m.CalculateTotalForChildren(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return 0;
            });
            mockReserve.Setup(m => m.CalculateTotalForSeniors(It.IsAny<ReservePriceDTO>())).Returns<ReservePriceDTO>((dto) =>
            {
                return 0;
            });
            var reserveService = new ReserveService(mockUOW.Object, mockReserve.Object);

            reserveService.Create(reserveModel);

            mockReserveRepository.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var reserveService = new ReserveService(mockUOW.Object,null);

            reserveService.Create(null);
        }

        [TestMethod]
        public void UpdateExistantReserve()
        {
            var reserveDescriptionModelIn = new ReserveDescriptionModelIn()
            {
                ReserveId = Guid.NewGuid(),
                Description = "Is ready to pay.",
                State = 1
            };

            var reserveDescription = new ReserveDescription()
            {
                ReserveId = Guid.NewGuid(),
                Description = "Is ready to pay.",
                State = ReserveState.PAYMENT_APPROVAL
            };

            var repoMock = new Mock<IReserveRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.UpdateState(It.IsAny<ReserveDescription>()));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.ReserveRepository).Returns(repoMock.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var service = new ReserveService(mockUOW.Object,null);

            service.UpdateState(reserveDescriptionModelIn);

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void UpdateNonExistantReserve()
        {
            var reserveDescriptionModelIn = new ReserveDescriptionModelIn()
            {
                ReserveId = Guid.NewGuid(),
                Description = "Is ready to pay.",
                State = 6
            };

            var reserveDescription = new ReserveDescription()
            {
                ReserveId = Guid.NewGuid(),
                Description = "Is ready to pay.",
                State = ReserveState.PAYMENT_APPROVAL
            };
            var repoMock = new Mock<IReserveRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.UpdateState(It.IsAny<ReserveDescription>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.ReserveRepository).Returns(repoMock.Object);
            var service = new ReserveService(mockUOW.Object,null);

            service.UpdateState(reserveDescriptionModelIn);

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void UpdateNull()
        {
            var repoMock = new Mock<IReserveRepository>(MockBehavior.Strict);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.ReserveRepository).Returns(repoMock.Object);
            var service = new ReserveService(mockUOW.Object,null);

            service.UpdateState(null);
        }

        private Lodging CreateLodging()
        {
            return new Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = new TouristLocation()
                {
                    Id = Guid.NewGuid()
                },
                Stars = 3,
                PricePerNight = 200,
                Available = true,
                Telephone = "0999979",
                InformationText = "Test information text"
            };
        }

        private ReserveModelIn CreateReserveModelIn(Guid lodginId)
        {
            return new ReserveModelIn()
            {
                LodgingId = lodginId,
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
                ContactFirstName = "Mark",
                ContactLastName = "Stromber",
                ContactEmail = "markstromber@hotmail.com"
            };
        }

        private LodgingPriceDTO createLodgingpriceDTO(ReserveModelIn reserveModelIn)
        {
            return new LodgingPriceDTO()
            {
                CheckIn = reserveModelIn.CheckIn,
                CheckOut = reserveModelIn.CheckOut,
                Adults = reserveModelIn.Adults,
                Babies = reserveModelIn.Babies,
                Children = reserveModelIn.Children
            };
        }

        private Reserve CreateReserve(Lodging lodging, ReserveModelIn reserveModelIn)
        {
            var lodgingpriceDTO = createLodgingpriceDTO(reserveModelIn);
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingpriceDTO,
                TotalDays = 2,
            };
            var reservePrice = new ReservePriceCalculation();
            
            return new Reserve()
            {
                Id = Guid.NewGuid(),
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
                ContactFirstName = "Mark",
                ContactLastName = "Ztruges",
                ContactEmail = "markztruges@hotmail.com",
                Price = reservePrice.TotalPriceOfStay(reserveDTO),
                Telephone = lodging.Telephone,
                InformationText = lodging.InformationText,
                LodgingId = lodging.Id,
                ReserveDescription = new ReserveDescription()
                {
                    ReserveId = Guid.NewGuid(),
                    Description = "Change reserve",
                    State = ReserveState.APPROVED
                }
            };
        }

        [TestMethod]
        public void GetById()
        {
            var reserveId = Guid.NewGuid();
            var lodging = CreateLodging();
            var reserveModelIn = CreateReserveModelIn(lodging.Id);
            var reserve = CreateReserve(lodging, reserveModelIn);
            reserve.Id = reserveId;

            var mockRepository = new Mock<IReserveRepository>(MockBehavior.Strict);
            mockRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(reserve);
            mockUOW.SetupGet(u => u.ReserveRepository).Returns(mockRepository.Object);
            IReserveService service = new ReserveService(mockUOW.Object, null);

            var result = service.GetById(reserveId);

            mockRepository.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(result.ReserveNumber == reserveId);
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

    }
}
