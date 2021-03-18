using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WeTravel.Domain;

namespace WeTravel.DataAccess.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SessionRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, null, null, null, null, null, null, new SessionRepository(context), null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddSession()
        {
            var session = new Session()
            {
                Token = Guid.NewGuid(),
                User = new User()
                {
                    Email = "test@test.com",
                    FullName = "Test",
                    Password = "test1234"
                }
            };

            uof.SessionRepository.Create(session);
            context.SaveChanges();

            var result = context.Set<Session>().ToList();
            Assert.IsTrue(new List<Session>(result).Contains(session));
        }

        [TestMethod]
        public void ValidToken()
        {
            var token = Guid.NewGuid();
            var session = new Session()
            {
                User = new User()
                {
                    Email = "test@test.com",
                    FullName = "Test",
                    Password = "test1234"
                },
                Token = token
            };
            context.Set<Session>().Add(session);
            context.SaveChanges();

            var result = uof.SessionRepository.ContainsToken(token);
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InvalidToken()
        {
            var token = Guid.NewGuid();

            var result = uof.SessionRepository.ContainsToken(token);

            Assert.IsFalse(result);
        }
    }
}