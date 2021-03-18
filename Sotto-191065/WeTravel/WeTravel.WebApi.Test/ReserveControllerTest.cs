using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using WeTravel.ServiceInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.WebApi.Controllers;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReserveControllerTest
    {
        [TestMethod]
        public void AddOK()
        {
            var reserveModel = new ReserveModelIn()
            {
                LodgingId = Guid.NewGuid(),
                CheckIn = new DateTime(2020, 10, 13),
                CheckOut = new DateTime(2020, 10, 15),
                Adults = 2,
                Children = 0,
                Babies = 0,
                ContactFirstName = "Mark",
                ContactLastName = "Stromber",
                ContactEmail = "markstromber@hotmail.com"
            };
            var reserveModelOut = new ReserveModelOut()
            {
                Id = Guid.NewGuid(),
                InformationText = "This is a test description for test 1",
                Telephone = "047892"
            };
            var mockBusinessLogic = new Mock<IReserveService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<ReserveModelIn>())).Returns(reserveModelOut);
            var controller = new ReserveController(mockBusinessLogic.Object);

            var obtainedResult = controller.Create(reserveModel);
            var contentResult = obtainedResult as OkObjectResult;
            mockBusinessLogic.VerifyAll();
            Assert.IsNotNull(contentResult.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNullModel()
        {
            var mockBusinessLogic = new Mock<IReserveService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<ReserveModelIn>()))
                .Throws(new ArgumentExceptionBeautifier(""));
            var controller = new ReserveController(mockBusinessLogic.Object);

            controller.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AddInvalidModel()
        {
            var reserveModel = new ReserveModelIn();

            var mockBusinessLogic = new Mock<IReserveService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<ReserveModelIn>()))
                .Throws(new FormatExceptionBeautifier(""));
            var controller = new ReserveController(mockBusinessLogic.Object);

            controller.Create(reserveModel);

            mockBusinessLogic.VerifyAll();
        }

        [TestMethod]
        public void GetLodgingById()
        {
            var reserveToFind = Guid.NewGuid();
            var reserveDescriptionModelOut = new ReserveDescriptionModelOut()
            {
                ReserveNumber = reserveToFind,
                ContactFullName = "Mark sgrognaf",
                Description = "The reservation is created and waiting to be pending payment.",
                State = ReserveState.CREATED.ToString()
            };
            var mockBusinessLogic = new Mock<IReserveService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((ReserveDescriptionModelOut)reserveDescriptionModelOut);
            var controller = new ReserveController(mockBusinessLogic.Object);

            var obtainedResult = controller.GetFromId(reserveToFind);
            
            var contentResult = obtainedResult as OkObjectResult;
            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(reserveToFind == (contentResult.Value as ReserveDescriptionModelOut).ReserveNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetLodgingByIdNonExistant()
        {
            var mockBusinessLogic = new Mock<IReserveService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetById(It.IsAny<Guid>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new ReserveController(mockBusinessLogic.Object);

            controller.GetFromId(Guid.NewGuid());
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
            var mockBusinessLogic = new Mock<IReserveService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.UpdateState(It.IsAny<ReserveDescriptionModelIn>()));
            var controller = new ReserveController(mockBusinessLogic.Object);

            var obtainedResult = controller.UpdateState(reserveDescriptionModelIn);
            
            var contentResult = obtainedResult as OkResult;
            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(contentResult.StatusCode == 200);
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
            var mockBusinessLogic = new Mock<IReserveService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.UpdateState(It.IsAny<ReserveDescriptionModelIn>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new ReserveController(mockBusinessLogic.Object);

            controller.UpdateState(reserveDescriptionModelIn);
        }
    }
}
