using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using WeTravel.DataAccessInterface;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.DataAccess.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, null, null, null, null, null, new UserRepository(context), null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddUser()
        {
            var user = new User()
            {
                FullName = "Test",
                Email = "test@test.com",
                Password = "test123"
            };

            uof.UserRepository.Create(user);
            context.SaveChanges();
            var result = uof.UserRepository.GetAll();

            Assert.IsTrue(new List<User>(result).Contains(user));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddUserRepeated()
        {
            var user = new User()
            {
                FullName = "Test",
                Email = "test@test.com",
                Password = "test123"
            };
            uof.UserRepository.Create(user);
            context.SaveChanges();

            uof.UserRepository.Create(user);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    FullName = "Test1",
                    Email = "test1@test.com",
                    Password = "test123"
                },
                new User()
                {
                    FullName = "Test2",
                    Email = "test2@test.com",
                    Password = "test1234"
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            var result = uof.UserRepository.GetAll();

            Assert.IsFalse(result.Except(users).Any());
        }

        [TestMethod]
        public void GetUserById()
        {
            var userToFind = new User()
            {
                FullName = "Test1",
                Email = "test1@test.com",
                Password = "test123"
            };
            var users = new List<User>()
            {
                userToFind,
                new User()
                {
                    FullName = "Test2",
                    Email = "test2@test.com",
                    Password = "test1234"
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            var result = uof.UserRepository.Get(userToFind);

            Assert.IsTrue(userToFind.Equals(result));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetUserByIdInvalid()
        {
            var userToFind = new User()
            {
                FullName = "Test1",
                Email = "test0@test.com",
                Password = "Test123"
            };
            var users = new List<User>()
            {
                new User()
                {
                    FullName = "Test1",
                    Email = "test1@test.com",
                    Password = "test123"
                },
                new User()
                {
                    FullName = "Test2",
                    Email = "test2@test.com",
                    Password = "test1234"
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            uof.UserRepository.Get(userToFind);
        }

        [TestMethod]
        public void DeleteOk()
        {
            var userId = "test@test.com";
            var lodging = new User()
            {
                FullName = "Test2",
                Email = userId,
                Password = "test1234"
            };
            context.Users.Add(lodging);
            context.SaveChanges();

            uof.UserRepository.Delete(userId);
            context.SaveChanges();

            Assert.IsFalse(context.Users.Contains(lodging));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void DeleteInvalid()
        {
            var userId = "test@test.com";

            uof.UserRepository.Delete(userId);
            context.SaveChanges();
        }

        [TestMethod]
        public void UpdateUser()
        {
            var user = new User()
            {
                FullName = "Test",
                Email = "test@test.com",
                Password = "test123"
            };
            context.Users.Add(user);
            context.SaveChanges();
            user.FullName = "New Name";

            uof.UserRepository.UpdateUser(user);
            context.SaveChanges();
            var result = uof.UserRepository.GetAll();

            Assert.IsTrue(((User)result.ToArray().GetValue(0)).FullName == user.FullName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void UpdateUserNonExistant()
        {
            var user = new User()
            {
                FullName = "Test",
                Email = "test@test.com",
                Password = "test123"
            };

            uof.UserRepository.UpdateUser(user);
            context.SaveChanges();
        }
    }
}
