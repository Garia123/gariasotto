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
    public class CategoryRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, new CategoryRepository(context), null, null, null, null, null, null,null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void Get()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Region 1"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Region 2"
                }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            var results = uof.CategoryRepository.Get();

            Assert.IsFalse(categories.Except(results).Any());
        }
    }
}
