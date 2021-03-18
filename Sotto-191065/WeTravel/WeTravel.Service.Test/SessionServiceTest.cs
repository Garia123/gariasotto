using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.Service.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SessionServiceTest
    {
        [TestMethod]
        public void Login()
        {
            var token = Guid.NewGuid();
            var email = "test@test.com";
            var password = "test123";
            var login = new LoginModelIn()
            {
                Email = email,
                Password = "test123"
            };
            var entity = new User()
            {
                FullName = "Name",
                Email = email,
                Password = password
            };
            var repoMockU = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMockU.Setup(r => r.Get(It.IsAny<User>())).Returns(entity);
            var repoMockS = new Mock<ISessionRepository>(MockBehavior.Strict);
            repoMockS.Setup(r => r.Create(It.IsAny<Session>()));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMockU.Object);
            mockUOW.SetupGet(u => u.SessionRepository).Returns(repoMockS.Object);
            mockUOW.Setup(u => u.Save()).Returns(0);
            var service = new SessionService(mockUOW.Object);

            service.Login(login);

            repoMockU.VerifyAll();
            repoMockS.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentExceptionBeautifier))]
        public void AddNull()
        {
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var service = new SessionService(mockUOW.Object);

            service.Login(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddInvalidModel()
        {
            var email = "test@test.com";
            var login = new LoginModelIn()
            {
                Email = email,
                Password = "test123"
            };
            var entity = new User()
            {
                FullName = "Name",
                Email = email,
                Password = "ok1234"
            };
            var repoMockU = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMockU.Setup(r => r.Get(It.IsAny<User>())).Returns(entity);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMockU.Object);
            var service = new SessionService(mockUOW.Object);

            service.Login(login);

            repoMockU.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void UserDoesNotExists()
        {
            var email = "test@test.com";
            var login = new LoginModelIn()
            {
                Email = email,
                Password = "test123"
            };
            var repoMockU = new Mock<IUserRepository>(MockBehavior.Strict);
            repoMockU.Setup(r => r.Get(It.IsAny<User>()))
                .Throws(new InvalidOperationExceptionBeautifier(""));
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.UserRepository).Returns(repoMockU.Object);
            var service = new SessionService(mockUOW.Object);

            service.Login(login);

            repoMockU.VerifyAll();
            mockUOW.VerifyAll();
        }

        [TestMethod]
        public void Token()
        {
            var token = Guid.NewGuid();
            var repoMock = new Mock<ISessionRepository>(MockBehavior.Strict);
            repoMock.Setup(r => r.ContainsToken(It.IsAny<Guid>())).Returns(true);
            var mockUOW = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mockUOW.SetupGet(u => u.SessionRepository).Returns(repoMock.Object);
            var service = new SessionService(mockUOW.Object);

            service.ValidateToken(token);

            repoMock.VerifyAll();
            mockUOW.VerifyAll();
        }
    }
}
