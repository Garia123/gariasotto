using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using WeTravel.Domain.Exceptions;

namespace WeTravel.Domain.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TouristLocationTest
    {
        [TestMethod]
        public void Valid()
        {
            Region region = new Region()
            {
                Id = Guid.NewGuid()
            };

            var touristLocation = new TouristLocation()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Description = "Test Description",
                Region = region,
                Lodgings = null,
                TouristLocationCategories = null,
            };

            touristLocation.ValidateEntity();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatExceptionBeautifier))]
        public void MissingId()
        {
            Region region = new Region()
            {
                Id = Guid.NewGuid()
            };

            var touristLocation = new TouristLocation()
            {
                Name = "Test Name",
                Description = "Test Description",
                Region = region,
                Lodgings = null,
                TouristLocationCategories = null,
            };

            touristLocation.ValidateEntity();
        }
    }
}
