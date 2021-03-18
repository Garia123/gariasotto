using System;
using System.Collections.Generic;
using MassLodgingImporter;
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
    public class MassLodgingServiceTest
    {
        private Mock<IUnitOfWork> mockUOW;

        [TestInitialize]
        public void init()
        {
            mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }
        
        [TestMethod]
        public void MassAddOk()
        {
            int impNumber = 2;
            string route = "routeTest";
            var lodgingWithId = getLodgingModelIn();
            var lodgingNoId = getLodgingModelIn();
            lodgingWithId.TouristLocationId = Guid.NewGuid();
            lodgingNoId.TouristLocationModel = new TouristLocationMassLodgingModel()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            var list = new List<LodgingMassLodgingModel>()
            {
                lodgingWithId,
                lodgingNoId
            };
            var mockLodgingService = new Mock<ILodgingService>();
            var mockTouristLocationService = new Mock<ITouristLocationService>();
            var mockLoadMassLodging = new Mock<ILoadMassLodgingAssembly>();
            var mockIAssembly = new Mock<IMassLodgingImporter>();
            mockLodgingService.Setup(m => m.Create(It.IsAny<LodgingModelIn>()));
            mockTouristLocationService.Setup(m => m.Create(It.IsAny<TouristLocationModelIn>()));
            mockTouristLocationService.Setup(m => m.GetTouristLocations(It.IsAny<TouristLocationModelFilter>())).Returns(new List<TouristLocationModelOut>(){new TouristLocationModelOut(){Id = Guid.NewGuid()}});
            mockLoadMassLodging.Setup(m => m.GetImplementation(It.IsAny<int>())).Returns(mockIAssembly.Object);
            mockIAssembly.Setup(m => m.GetElements(It.IsAny<string>())).Returns(list);
            var massLodgingService = new MassLodgingService(mockLodgingService.Object,mockTouristLocationService.Object,mockLoadMassLodging.Object);

            massLodgingService.MassCreate(impNumber,route);
            
            mockLodgingService.VerifyAll();
            mockTouristLocationService.VerifyAll();
            mockLoadMassLodging.VerifyAll();
            mockIAssembly.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void MassAddInvalidImp()
        {
            int impNumber = 2;
            string route = "routeTest";
            var lodgingWithId = getLodgingModelIn();
            var lodgingNoId = getLodgingModelIn();
            lodgingWithId.TouristLocationId = Guid.NewGuid();
            lodgingNoId.TouristLocationModel = new TouristLocationMassLodgingModel()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            var list = new List<LodgingMassLodgingModel>()
            {
                getLodgingModelIn(),
                getLodgingModelIn()
            };
            var mockLoadMassLodging = new Mock<ILoadMassLodgingAssembly>();
            var mockIAssembly = new Mock<IMassLodgingImporter>();
            mockLoadMassLodging.Setup(m => m.GetImplementation(It.IsAny<int>())).Throws(new InvalidOperationExceptionBeautifier(""));
            mockIAssembly.Setup(m => m.GetElements(It.IsAny<string>())).Returns(list);
            var massLodgingService = new MassLodgingService(null,null,mockLoadMassLodging.Object);

            massLodgingService.MassCreate(impNumber,route);
            
            mockLoadMassLodging.VerifyAll();
            mockIAssembly.VerifyAll();
        }

        [TestMethod]
        public void MassAddGet()
        {
            int impNumber = 2;
            string route = "routeTest";
            var lodgingWithId = getLodgingModelIn();
            var lodgingNoId = getLodgingModelIn();
            lodgingWithId.TouristLocationId = Guid.NewGuid();
            lodgingNoId.TouristLocationModel = new TouristLocationMassLodgingModel()
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            var list = new List<LodgingMassLodgingModel>()
            {
                getLodgingModelIn(),
                getLodgingModelIn()
            };
            var mockLoadMassLodging = new Mock<ILoadMassLodgingAssembly>();
            mockLoadMassLodging.Setup(m => m.GetImplementations()).Returns(new List<Type>() {typeof(Type),typeof(Type)});
            var massLodgingService = new MassLodgingService(null,null,mockLoadMassLodging.Object);

            massLodgingService.GetImplementationsForMassAdd();
            
            mockLoadMassLodging.VerifyAll();
        }

        private LodgingMassLodgingModel getLodgingModelIn()
        {
            return new LodgingMassLodgingModel()
            {
                Name = "Lodging Test 2",
                Stars = 2,
                Description = "This is a test description",
                Address = "test address",
                PricePerNight = 250,
                Available = true,
                Telephone = "123121",
                InformationText = "Information test Example",
            };
        }
    }
}