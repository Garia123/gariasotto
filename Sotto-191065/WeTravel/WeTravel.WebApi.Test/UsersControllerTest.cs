using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using WeTravel.ServiceInterface;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;
using WeTravel.WebApi.Controllers;

namespace WeTravel.WebApi.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UsersControllerTest
    {
        [TestMethod]
        public void CreateNewUser()
        {
            var model = new UserModelIn()
            {
                FullName = "Name",
                Email = "test@test.com",
                Password = "test123"
            };
            var mockBusinessLogic = new Mock<IUserService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<UserModelIn>()));
            var controller = new UserController(mockBusinessLogic.Object);

            var obtainedResult = controller.Create(model);
            var contentResult = obtainedResult as OkResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var mockBusinessLogic = new Mock<IUserService>(MockBehavior.Strict);
            var controller = new UserController(mockBusinessLogic.Object);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<UserModelIn>())).Throws(new ArgumentExceptionBeautifier(""));

            controller.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void AddInvalidModel()
        {
            var touristModel = new UserModelIn()
            {
                FullName = "test"
            };
            var mockBusinessLogic = new Mock<IUserService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Create(It.IsAny<UserModelIn>()))
                .Throws(new FormatExceptionBeautifier(""));
            var controller = new UserController(mockBusinessLogic.Object);

            controller.Create(touristModel);
        }

        [TestMethod]
        public void DeleteExistantUser()
        {
            var email = "test@test.com";
            var mockBusinessLogic = new Mock<IUserService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Delete(It.IsAny<string>()));
            var controller = new UserController(mockBusinessLogic.Object);

            var obtainedResult = controller.Delete(email);
            var contentResult = obtainedResult as OkResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(contentResult.StatusCode == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void DeleteNonExistantUser()
        {
            var email = "test@test.com";
            var mockBusinessLogic = new Mock<IUserService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.Delete(It.IsAny<string>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new UserController(mockBusinessLogic.Object);

            controller.Delete(email);
        }

        [TestMethod]
        public void EditExistantUser()
        {
            var updatedUserData = new UserModelIn()
            {
                Email = "test@tes.com",
                FullName = "New Name",
                Password = "newPassword"
            };
            var mockBusinessLogic = new Mock<IUserService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.UpdateUser(It.IsAny<UserModelIn>()));
            var controller = new UserController(mockBusinessLogic.Object);

            var obtainedResult = controller.UpdateAvailable(updatedUserData);
            var contentResult = obtainedResult as OkResult;

            mockBusinessLogic.VerifyAll();
            Assert.IsTrue(contentResult.StatusCode == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void EditNonExistantUser()
        {
            var updatedUserData = new UserModelIn()
            {
                Email = "test@tes.com",
                FullName = "New Name",
                Password = "newPassword"
            };
            var mockBusinessLogic = new Mock<IUserService>(MockBehavior.Strict);
            mockBusinessLogic.Setup(r => r.UpdateUser(It.IsAny<UserModelIn>())).Throws(new InvalidOperationExceptionBeautifier(""));
            var controller = new UserController(mockBusinessLogic.Object);

            controller.UpdateAvailable(updatedUserData);
        }

        [TestMethod]
        public void GetUsers()
        {
            var users = new List<UserModelOut>()
            {
                new UserModelOut()
                {
                    FullName = "Test1",
                    Email ="test1@test.com"
                },
                new UserModelOut()
                {
                    FullName = "Test2",
                    Email ="test2@test.com"
                }
            };
            var mock = new Mock<IUserService>(MockBehavior.Strict);
            mock.Setup(m => m.Get()).Returns(users);
            var controller = new UserController(mock.Object);

            var result = controller.Get();
            var results = result as OkObjectResult;

            mock.VerifyAll();
            Assert.IsTrue(users.SequenceEqual(results.Value as IEnumerable<UserModelOut>));
        }
    }
}
