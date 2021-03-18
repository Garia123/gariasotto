using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.DataAccess.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReserveRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, null, null, null, new ReserveRepository(context), null, null, null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddReserve()
        {
            var touristLocation = CreateTouristLocation();
            var lodging = CreateLodging(touristLocation);
            var reserve = CreateReserve(lodging);

            context.TouristLocations.Add(touristLocation);
            context.Lodgings.Add(lodging);
            uof.ReserveRepository.Create(reserve);
            context.SaveChanges();

            var result = uof.ReserveRepository.GetById(reserve.Id);

            Assert.IsTrue(result.Equals(reserve));
        }

        [TestMethod]
        public void GetReserveById()
        {
            var touristLocation = CreateTouristLocation();
            var lodging = CreateLodging(touristLocation);
            var reserve = CreateReserve(lodging);

            context.TouristLocations.Add(touristLocation);
            context.Lodgings.Add(lodging);
            context.Reserves.Add(reserve);
            context.SaveChanges();

            var reserveId = reserve.Id;

            var result = uof.ReserveRepository.GetById(reserveId);

            Assert.IsTrue(result.Equals(reserve));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetReserveByIdNonExistant()
        {
            var reserveId = Guid.NewGuid();

            uof.ReserveRepository.GetById(reserveId);
        }

        [TestMethod]
        public void UpdateExistantReserve()
        {
            var touristLocation = CreateTouristLocation();
            var lodging = CreateLodging(touristLocation);
            var reserve = CreateReserve(lodging);
            var reserveDescription = reserve.ReserveDescription;

            context.Lodgings.Add(lodging);
            context.Reserves.Add(reserve);
            context.ReserveDescriptions.Add(reserveDescription);
            context.SaveChanges();

            reserve.ReserveDescription.State = ReserveState.PAYMENT_APPROVAL;
            reserve.ReserveDescription.Description = "Change to payment approval";

            uof.ReserveRepository.UpdateState(reserve.ReserveDescription);
            context.SaveChanges();
            var result = uof.ReserveRepository.GetById(reserve.Id);

            Assert.IsTrue(result.Id == reserve.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void UpdateNonExistantReserve()
        {
            var reserveDescription = new ReserveDescription()
            {
                ReserveId = Guid.NewGuid(),
                Description = "Change reserve",
                State = ReserveState.APPROVED
            };

            uof.ReserveRepository.UpdateState(reserveDescription);
            context.SaveChanges();
        }

        private TouristLocation CreateTouristLocation()
        {
            return new TouristLocation()
            {
                Id = Guid.NewGuid()
            };
        }

        private Domain.Lodging CreateLodging(Domain.TouristLocation touristLocation)
        {
            return new Domain.Lodging()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Address = "Test address",
                TouristLocation = touristLocation,
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
                CheckIn = DateTime.Now.AddDays(-20),
                CheckOut = DateTime.Now,
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

        private Domain.Reserve CreateReserve(Domain.Lodging lodging)
        {
            var reserveModelIn = CreateReserveModelIn(lodging.Id);
            var lodgingpriceDTO = createLodgingpriceDTO(reserveModelIn);
            var reserveDTO = new ReservePriceDTO()
            {
                LodgingPriceDTO = lodgingpriceDTO,
                TotalDays = 2,
            };
            var reservePrice = new ReservePriceCalculation();

            return new Domain.Reserve()
            {
                Id = Guid.NewGuid(),
                CheckIn = DateTime.Now.AddDays(-20),
                CheckOut = DateTime.Now,
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
        public void GetReservesFromFilter()
        {
            var touristLocation = CreateTouristLocation();
            var lodging = CreateLodging(touristLocation);
            var reserve1 = CreateReserve(lodging);
            var reserve2 = CreateReserve(lodging);
            var reservesExpecteds = new List<Domain.Reserve>()
            {
                reserve1,
                reserve2,
            };
            TouristLocationReportFilter filter = new TouristLocationReportFilter()
            {
                TouristLocationName = lodging.Name,
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now
            };
            reserve2.Id = Guid.NewGuid();
            context.TouristLocations.Add(touristLocation);
            context.Lodgings.Add(lodging);
            context.Reserves.Add(reserve1);
            context.Reserves.Add(reserve2);
            context.SaveChanges();

            var result = uof.ReserveRepository.GetReserves(filter);

            Assert.IsTrue(reservesExpecteds.SequenceEqual(result));
        }
    }
}
