using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using WeTravel.ServiceInterface;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.Service.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserServiceTest
    {
        [TestMethod]
        public void CreateNewUser()
        {
            var model = new UserModelIn()
            {
                FullName = "Name",
                Email = "test@test.com",
                Password = "Test1234"
            };
            var expectedResult = new UserModelOut()
            {
                FullName = "Name",
                Email = "test@test.com"
            };
            var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Create(It.IsAny<User>()));
            repoMock.Setup(r => r.GetAll()).Returns(new List<User>() {
                new User()
                {
                    FullName = "Name",
                    Email = "test@test.com",
                    Password = "Test1234"
                }
            });
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMock.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var service = new UserService(mockUOW.Object);

            service.Create(model);

            var result = service.Get();
            mockUOW.VerifyAll();
            repoMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.IsTrue(((UserModelOut)result.ToArray()
                .GetValue(0)).Email == expectedResult.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMock.Object);
            var service = new UserService(mockUOW.Object);

            service.Create(null);

            mockUOW.VerifyAll();
            repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void DeleteExistantUser()
        {
            var email = "test@test.com";
            var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Delete(It.IsAny<string>()));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMock.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var service = new UserService(mockUOW.Object);

            service.Delete(email);

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void DeleteNonExistantUser()
        {
            var email = "test@test.com";
            var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.Delete(It.IsAny<string>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMock.Object);
            var service = new UserService(mockUOW.Object);

            service.Delete(email);

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        public void EditExistantUser()
        {
            var updatedUserData = new UserModelIn()
            {
                Email = "test@tes.com",
                FullName = "New Name",
                Password = "Test1234123"
            };
            var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.UpdateUser(It.IsAny<User>()));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMock.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var service = new UserService(mockUOW.Object);

            service.UpdateUser(updatedUserData);

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void EditNonExistantUser()
        {
            var updatedUserData = new UserModelIn()
            {
                Email = "test@tes.com",
                FullName = "New Name",
                Password = "Clave123"
            };
            var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.UpdateUser(It.IsAny<User>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMock.Object);
            var service = new UserService(mockUOW.Object);

            service.UpdateUser(updatedUserData);

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        public void GetUsers()
        {
            var email1 = "test1@test.com";
            var email2 = "test2@test.com";
            var users = new List<User>()
            {
                new User()
                {
                    FullName = "Test1",
                    Email = email1,
                    Password = "Test123"
                },
                new User()
                {
                    FullName = "Test2",
                    Email = email2,
                    Password = "Test123"
                }
            };
            var repoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMock.Setup(m => m.GetAll()).Returns(users);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMock.Object);
            var service = new UserService(mockUOW.Object);

            var result = service.Get();

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
            Assert.IsTrue(((UserModelOut)result.ToArray().GetValue(0)).Email == email1);
            Assert.IsTrue(((UserModelOut)result.ToArray().GetValue(1)).Email == email2);
        }
    }
}
