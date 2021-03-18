using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WeTravel.Domain;
using WeTravel.Domain.Exceptions;
using WeTravel.Model;

namespace WeTravel.DataAccess.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TouristLocationRepositoryTest
    {
        private UnitOfWork uof;
        private WeTravelDbContext context;
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeTravelDbContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            context = new WeTravelDbContext(options);
            uof = new UnitOfWork(context, null, null, null, null, new TouristLocationRepository(context, new TouristLocationFilter()), null, null, null);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddTouristLocation()
        {
            var categoryId = Guid.NewGuid();
            var region = new Region()
            {
                Id = Guid.NewGuid()
            };
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = categoryId,
                    Name = "Beach"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Historic"
                }
            };
            var touristId = Guid.NewGuid();
            var tourist = new Domain.TouristLocation()
            {
                Id = touristId,
                Name = "Test Location",
                Description = "Test Description",
                Region = region,
                TouristLocationCategories = new List<TouristLocationCategory>(){
                    new TouristLocationCategory()
                    {
                        CategoryId = categoryId,
                        TouristLocationId = touristId,
                    }
                }
            };
            context.Categories.AddRange(categories);
            context.Regions.Add(region);
            context.SaveChanges();

            uof.TouristLocationRepository.Create(tourist);
            context.SaveChanges();
            var result = uof.TouristLocationRepository.Get(null);

            Assert.IsTrue(new List<Domain.TouristLocation>(result).Contains(tourist));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddTouristLocationRepeated()
        {
            var categoryId = Guid.NewGuid();
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = categoryId,
                    Name = "Beach"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Historic"
                }
            };
            var touristId = Guid.NewGuid();
            var tourist = new Domain.TouristLocation()
            {
                Id = touristId,
                Name = "Test Location",
                Description = "Test Description",
                Region = new Region()
                {
                    Id = Guid.NewGuid()
                },
                TouristLocationCategories = new List<TouristLocationCategory>(){
                    new TouristLocationCategory()
                    {
                        CategoryId = categoryId,
                        TouristLocationId = touristId
                    }
                }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            uof.TouristLocationRepository.Create(tourist);
            context.SaveChanges();
            uof.TouristLocationRepository.Create(tourist);
            context.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddTouristLocationNoCategories()
        {
            var touristId = Guid.NewGuid();
            var region = new Region()
            {
                Id = Guid.NewGuid()
            };
            var tourist = new Domain.TouristLocation()
            {
                Id = touristId,
                Name = "Test Location",
                Description = "Test Description",
                Region = region,
                TouristLocationCategories = new List<TouristLocationCategory>(){
                    new TouristLocationCategory()
                    {
                        CategoryId = Guid.NewGuid(),
                        TouristLocationId = touristId
                    }
                }
            };
            context.Regions.Add(region);
            context.SaveChanges();
            uof.TouristLocationRepository.Create(tourist);
            context.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void AddTouristLocationNoRegion()
        {
            var categoryId = Guid.NewGuid();
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = categoryId,
                    Name = "Beach"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Historic"
                }
            };
            var touristId = Guid.NewGuid();
            var tourist = new Domain.TouristLocation()
            {
                Id = touristId,
                Name = "Test Location",
                Description = "Test Description",
                Region = new Region()
                {
                    Id = Guid.NewGuid()
                },
                TouristLocationCategories = new List<TouristLocationCategory>(){
                    new TouristLocationCategory()
                    {
                        CategoryId = categoryId,
                        TouristLocationId = touristId
                    }
                }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            uof.TouristLocationRepository.Create(tourist);
            context.SaveChanges();
        }

        [TestMethod]
        public void GetAllTouristLocation()
        {
            var categoryId = Guid.NewGuid();
            var region = new Region()
            {
                Id = Guid.NewGuid()
            };
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = categoryId,
                    Name = "Beach"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Historic"
                }
            };
            var touristId1 = Guid.NewGuid();
            var touristId2 = Guid.NewGuid();
            var touristLocations = new List<Domain.TouristLocation>()
            {
                new Domain.TouristLocation()
                {
                    Id = touristId1,
                    Name = "Test Location 1",
                    Description = "Test Description 1",
                    Region = region,
                    TouristLocationCategories = new List<TouristLocationCategory>(){
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId,
                            TouristLocationId = touristId1
                        }
                    }
                },
                new Domain.TouristLocation()
                {
                    Id = touristId2,
                    Name = "Test Location 2",
                    Description = "Test Description 2",
                    Region = region,
                    TouristLocationCategories = new List<TouristLocationCategory>() {
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId,
                            TouristLocationId = touristId2
                        }
                    }
                }

            };
            context.Categories.AddRange(categories);
            context.TouristLocations.AddRange(touristLocations);
            context.SaveChanges();

            var result = uof.TouristLocationRepository.Get(new TouristLocationModelFilter());

            Assert.IsFalse(result.Except(touristLocations).Any());
        }

        [TestMethod]
        public void GetAllTouristLocationWithCategoryRegionFilter()
        {
            var categoryId1 = Guid.NewGuid();
            var categoryId2 = Guid.NewGuid();
            var region = new Region()
            {
                Id = Guid.NewGuid()
            };
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = categoryId1,
                    Name = "Beach"
                },
                new Category()
                {
                    Id = categoryId2,
                    Name = "Historic"
                }
            };
            var touristId1 = Guid.NewGuid();
            var touristId2 = Guid.NewGuid();
            var touristLocations = new List<Domain.TouristLocation>()
            {
                new Domain.TouristLocation()
                {
                    Id = touristId1,
                    Name = "Test Location 1",
                    Description = "Test Description 1",
                    Region = region,
                    TouristLocationCategories = new List<TouristLocationCategory>(){
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId2,
                            TouristLocationId = touristId1
                        }
                    }
                },
                new Domain.TouristLocation()
                {
                    Id = touristId2,
                    Name = "Test Location 2",
                    Description = "Test Description 2",
                    Region = region,
                    TouristLocationCategories = new List<TouristLocationCategory>() {
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId1,
                            TouristLocationId = touristId2
                        }
                    }
                }
            };
            context.Categories.AddRange(categories);
            context.TouristLocations.AddRange(touristLocations);
            context.SaveChanges();
            var filter = new TouristLocationModelFilter()
            {
                RegionId = region.Id,
                CategoryId = categoryId1
            };
            var expected = new List<Domain.TouristLocation>()
            {
                (Domain.TouristLocation) touristLocations.ToArray().GetValue(1)
            };
            var result = uof.TouristLocationRepository.Get(filter);

            Assert.IsFalse(result.Except(expected).Any());
        }

        [TestMethod]
        public void GetAllTouristLocationWithCategoryFilter()
        {
            var categoryId1 = Guid.NewGuid();
            var categoryId2 = Guid.NewGuid();
            var region = new Region()
            {
                Id = Guid.NewGuid()
            };
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = categoryId1,
                    Name = "Beach"
                },
                new Category()
                {
                    Id = categoryId2,
                    Name = "Historic"
                }
            };
            var touristId1 = Guid.NewGuid();
            var touristId2 = Guid.NewGuid();
            var touristLocations = new List<Domain.TouristLocation>()
            {
                new Domain.TouristLocation()
                {
                    Id = touristId1,
                    Name = "Test Location 1",
                    Description = "Test Description 1",
                    Region = region,
                    TouristLocationCategories = new List<TouristLocationCategory>(){
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId2,
                            TouristLocationId = touristId1
                        }
                    }
                },
                new Domain.TouristLocation()
                {
                    Id = touristId2,
                    Name = "Test Location 2",
                    Description = "Test Description 2",
                    Region = new Region(){
                        Id = Guid.NewGuid()
                    },
                    TouristLocationCategories = new List<TouristLocationCategory>() {
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId1,
                            TouristLocationId = touristId2
                        }
                    }
                }
            };
            context.Categories.AddRange(categories);
            context.TouristLocations.AddRange(touristLocations);
            context.SaveChanges();
            var filter = new TouristLocationModelFilter()
            {
                CategoryId = categoryId1
            };
            var expected = new List<Domain.TouristLocation>()
            {
                (Domain.TouristLocation) touristLocations.ToArray().GetValue(1)
            };
            var result = uof.TouristLocationRepository.Get(filter);

            Assert.IsFalse(result.Except(expected).Any());
        }

        [TestMethod]
        public void GetAllTouristLocationWithRegionFilter()
        {
            var categoryId1 = Guid.NewGuid();
            var categoryId2 = Guid.NewGuid();
            var region = new Region()
            {
                Id = Guid.NewGuid()
            };
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = categoryId1,
                    Name = "Beach"
                },
                new Category()
                {
                    Id = categoryId2,
                    Name = "Historic"
                }
            };
            var touristId1 = Guid.NewGuid();
            var touristId2 = Guid.NewGuid();
            var touristLocations = new List<Domain.TouristLocation>()
            {
                new Domain.TouristLocation()
                {
                    Id = touristId1,
                    Name = "Test Location 1",
                    Description = "Test Description 1",
                    Region = region,
                    TouristLocationCategories = new List<TouristLocationCategory>(){
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId2,
                            TouristLocationId = touristId1
                        }
                    }
                },
                new Domain.TouristLocation()
                {
                    Id = touristId2,
                    Name = "Test Location 2",
                    Description = "Test Description 2",
                    Region = region,
                    TouristLocationCategories = new List<TouristLocationCategory>() {
                        new TouristLocationCategory()
                        {
                            CategoryId = categoryId1,
                            TouristLocationId = touristId2
                        }
                    }
                }
            };
            context.Categories.AddRange(categories);
            context.TouristLocations.AddRange(touristLocations);
            context.SaveChanges();
            var filter = new TouristLocationModelFilter()
            {
                RegionId = region.Id,
            };
            var expected = touristLocations;
            var result = uof.TouristLocationRepository.Get(filter);

            Assert.IsFalse(result.Except(expected).Any());
        }

        [TestMethod]
        public void GetTouristLocationById()
        {
            var touristLocation = new Domain.TouristLocation()
            {
                Id = Guid.NewGuid()
            };
            var touristId = Guid.NewGuid();
            var touristToFind = new Domain.TouristLocation()
            {
                Id = touristId,
                Name = "Test Location1",
                Description = "Test Description1",
            };
            var touristLocations = new List<Domain.TouristLocation>()
            {
                touristToFind,
                new Domain.TouristLocation()
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Location2",
                    Description = "Test Description2",
                }
            };
            context.TouristLocations.AddRange(touristLocations);
            context.SaveChanges();

            var result = uof.TouristLocationRepository.GetById(touristId);

            Assert.IsTrue(result.Equals(touristToFind));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationExceptionBeautifier))]
        public void GetTouristLocationByIdNonExistant()
        {
            var touristId = Guid.NewGuid();

            uof.TouristLocationRepository.GetById(touristId);
        }
    }
}