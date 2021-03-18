using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeTravel.ServiceInterface;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.WebApi.Controllers;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    public class MassLodgingControllerTest
    {
        [TestMethod]
        public void MassAddOk()
        {
            var imp = 9;
            var resourceLocation = "testFile";
            var mockBusinessLogic = new Mock<IMassLodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.MassCreate(9,resourceLocation));
            var controller = new MassLodgingController(mockBusinessLogic.Object);

            controller.MassCreate(9,"testFile");

            mockBusinessLogic.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void MassAddInvalidImp()
        {
            var imp = 9;
            var resourceLocation = "testFile";
            var mockBusinessLogic = new Mock<IMassLodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.MassCreate(9,resourceLocation))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new MassLodgingController(mockBusinessLogic.Object);

            controller.MassCreate(9,"testFile");

            mockBusinessLogic.VerifyAll();
        }

        [TestMethod]
        public void MassAddGet()
        {
            var mockBusinessLogic = new Mock<IMassLodgingService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.GetImplementationsForMassAdd()).Returns(new List<LodgingMassImpsModelOut>());
            var controller = new MassLodgingController(mockBusinessLogic.Object);

            controller.MassCreateGetImps();

            mockBusinessLogic.VerifyAll();
        }

    }
}