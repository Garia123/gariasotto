using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WeTravel.ServiceInterface;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.WebApi.Controllers;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    public class ReviewControllerTest
    {
        [TestMethod]
        public void AddOK()
        {
            var model = new ReviewModelIn()
            {
                ReserveId = Guid.NewGuid(),
                Rating = 2,
                Description = "This is a test for reviews"
            };
            var mockBusinessLogic = new Mock<IReviewService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<ReviewModelIn>()));
            var controller = new ReviewController(mockBusinessLogic.Object);

            var obtainedResult = controller.Create(model);

            var contentResult = obtainedResult as OkObjectResult;
            mockBusinessLogic.VerifyAll();
        }
    }
}
