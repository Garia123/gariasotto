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
    public class LodgingRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, null, new LodgingRepository(context, new LodgingFilter()), null, null, null, null, null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddLodging()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodging = new Domain.Lodging()
            {
                Id = lodgingId,
                Name = "Test Location",
                Description = "Test Description",
                Address = "Test Address",
                TouristLocation = touristLocation,
                Stars = 3
            };
            context.TouristLocations.Add(touristLocation);
            context.SaveChanges();

            uof.LodgingRepository.Create(lodging);
            context.SaveChanges();
            var result = uof.LodgingRepository.Get(new LodgingModelFilter());

            Assert.IsTrue(new List<Domain.Lodging>(result).Contains(lodging));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddTouristLocationRepeated()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodging = new Domain.Lodging()
            {
                Id = lodgingId,
                Name = "Test Location",
                Description = "Test Description",
                Address = "Test Address",
                TouristLocation = touristLocation,
                Stars = 3
            };
            context.TouristLocations.Add(touristLocation);
            context.SaveChanges();

            uof.LodgingRepository.Create(lodging);
            context.SaveChanges();
            uof.LodgingRepository.Create(lodging);
            context.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddLodgingNoTouristLocation()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodging = new Domain.Lodging()
            {
                Id = lodgingId,
                Name = "Test Location",
                Description = "Test Description",
                Address = "Test Address",
                TouristLocation = touristLocation,
                Stars = 3
            };

            uof.LodgingRepository.Create(lodging);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetAllLodgings()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodgings = new List<Domain.Lodging>()
            {
                new Domain.Lodging()
                {
                    Id = lodgingId,
                    Name = "Test Location",
                    Description = "Test Description",
                    Address = "Test Address",
                    TouristLocation = touristLocation,
                    Stars = 3
                },
                new Domain.Lodging()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Location",
                    Description = "Test Description",
                    Address = "Test Address",
                    TouristLocation = touristLocation,
                    Stars = 3
                }
            };
            context.TouristLocations.Add(touristLocation);
            context.SaveChanges();
            context.Lodgings.AddRange(lodgings);
            context.SaveChanges();

            var result = uof.LodgingRepository.Get(new LodgingModelFilter());

            Assert.IsFalse(result.Except(lodgings).Any());
        }

        [TestMethod]
        public void GetLodgingWithTouristLocation()
        {
            var touristLocation1 = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var touristLocation2 = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodgings = new List<Domain.Lodging>()
            {
                new Domain.Lodging()
                {
                    Id = lodgingId,
                    Name = "Test Location",
                    Description = "Test Description",
                    Address = "Test Address",
                    TouristLocation = touristLocation1,
                    Stars = 3
                },
                new Domain.Lodging()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Location",
                    Description = "Test Description",
                    Address = "Test Address",
                    TouristLocation = touristLocation2,
                    Stars = 3
                }
            };
            context.TouristLocations.Add(touristLocation1);
            context.TouristLocations.Add(touristLocation2);
            context.SaveChanges();
            context.Lodgings.AddRange(lodgings);
            context.SaveChanges();

            var result = uof.LodgingRepository.Get(new LodgingModelFilter() { TouristLocationId = touristLocation1.Id });

            Assert.IsFalse(result.Except(new List<Domain.Lodging>() { lodgings.ToArray().ElementAt(0) }).Any());
        }

        [TestMethod]
        public void DeleteOk()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodging = new Domain.Lodging()
            {
                Id = lodgingId,
                Name = "Test Location",
                Description = "Test Description",
                Address = "Test Address",
                TouristLocation = touristLocation,
                Stars = 3
            };
            context.TouristLocations.Add(touristLocation);
            context.SaveChanges();
            uof.LodgingRepository.Create(lodging);
            context.SaveChanges();

            uof.LodgingRepository.Delete(lodgingId);
            context.SaveChanges();

            Assert.IsFalse(context.Lodgings.Where(l => l.Id == lodging.Id).FirstOrDefault().Available);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void DeleteInvalid()
        {
            var lodgingId = Guid.NewGuid();

            uof.LodgingRepository.Delete(lodgingId);
            context.SaveChanges();
        }

        [TestMethod]
        public void ChangeStatusOk()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodging = new Domain.Lodging()
            {
                Id = lodgingId,
                Name = "Test Location",
                Description = "Test Description",
                Address = "Test Address",
                TouristLocation = touristLocation,
                Stars = 3
            };
            context.TouristLocations.Add(touristLocation);
            context.SaveChanges();
            uof.LodgingRepository.Create(lodging);
            context.SaveChanges();

            uof.LodgingRepository.ChangeStatus(lodgingId);
            context.SaveChanges();

            Assert.IsTrue(context.Lodgings.Where(l => l.Id == lodging.Id).FirstOrDefault().Available);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void ChangeStatusNonExistant()
        {
            var lodgingId = Guid.NewGuid();

            uof.LodgingRepository.ChangeStatus(lodgingId);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetLodgingById()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var lodgingId = Guid.NewGuid();
            var lodgingToFind = new Domain.Lodging()
            {
                Id = lodgingId,
                Name = "Test Location",
                Description = "Test Description",
                Address = "Test Address",
                TouristLocation = touristLocation,
                Stars = 3
            };
            var lodgings = new List<Domain.Lodging>()
            {
                lodgingToFind,
                new Domain.Lodging()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Location",
                    Description = "Test Description",
                    Address = "Test Address",
                    TouristLocation = touristLocation,
                    Stars = 3
                }
            };
            context.TouristLocations.Add(touristLocation);
            context.SaveChanges();
            context.Lodgings.AddRange(lodgings);
            context.SaveChanges();

            var result = uof.LodgingRepository.GetById(lodgingId);

            Assert.IsTrue(result.Equals(lodgingToFind));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetLodgingByIdNonExistant()
        {
            var lodgingId = Guid.NewGuid();

            uof.LodgingRepository.GetById(lodgingId);
        }
    }
}
