using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using WeTravel.ServiceInterface;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.WebApi.Controllers;
using WeTravel.WebApi.Controllers.Controllers;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SessionControllerTest
    {
        [TestMethod]
        public void Login()
        {
            var token = Guid.NewGuid();
            var model = new LoginModelIn()
            {
                Email = "test@test.com",
                Password = "test123"
            };
            var mockBusinessLogic = new Mock<ISessionService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Login(It.IsAny<LoginModelIn>())).Returns(token);
            var controller = new SessionController(mockBusinessLogic.Object);

            var obtainedResult = controller.Login(model);

            var contentResult = obtainedResult as OkObjectResult;
            mockBusinessLogic.VerifyAll();
            Assert.AreEqual(token, contentResult.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var mockBusinessLogic = new Mock<ISessionService>(MockBehavior.Strict);
            var controller = new SessionController(mockBusinessLogic.Object);
            mockBusinessLogic.Setup(r => r.Login(It.IsAny<LoginModelIn>())).Throws(new ArgumentExceptionBeautifier(""));

            controller.Login(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AddInvalidModel()
        {
            var model = new LoginModelIn()
            {
                Email = "test@test.com",
            };
            var mockBusinessLogic = new Mock<ISessionService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Login(It.IsAny<LoginModelIn>()))
                .Throws(new FormatExceptionBeautifier(""));
            var controller = new SessionController(mockBusinessLogic.Object);

            controller.Login(model);
        }
    }
}
