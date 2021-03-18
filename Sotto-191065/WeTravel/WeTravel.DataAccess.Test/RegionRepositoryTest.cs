using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using WeTravel.Domain;

namespace WeTravel.DataAccess.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RegionRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, null,null, new RegionRepository(context), null, null, null, null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void Get()
        {
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Region 1"
                },
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Region 2"
                }
            };
            context.Regions.AddRange(regions);
            context.SaveChanges();

            var results = uof.RegionRepository.Get();

            Assert.IsFalse(regions.Except(results).Any());
        }
    }
}
